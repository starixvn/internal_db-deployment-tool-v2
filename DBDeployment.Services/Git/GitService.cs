// --------------------------------------------------------------------------
// <copyright file="GitService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.16">Created</change>
// </history>

namespace DBDeployment.Services.Git
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using DBDeployment.Core.Utilities;
    using DBDeployment.Models.Gits;
    using NGit;
    using NGit.Api;
    using NGit.Diff;
    using NGit.Revplot;
    using NGit.Revwalk;
    using NGit.Transport;
    using NGit.Treewalk;
    using NGit.Treewalk.Filter;
    using NGit.Util.IO;

    /// <summary>
    /// GIT Service class.
    /// </summary>
    public class GitService : IGitService
    {
        /// <summary>
        /// Commits the specified GIT directory.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="commitMessage">The commit message.</param>
        /// <returns>Commit hash.</returns>
        public string Commit(string gitDirectory, string commitMessage)
        {
            gitDirectory = gitDirectory.Trim();
            var dir = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(dir);

            try
            {
                var differences = git.Diff()
                       .SetShowNameAndStatusOnly(true)
                       .Call();
                foreach (var diff in differences)
                {
                    var changedType = diff.GetChangeType();
                    if (changedType == DiffEntry.ChangeType.DELETE)
                    {
                        git.Rm().AddFilepattern(diff.GetOldPath()).Call();
                    }
                    else if (changedType == DiffEntry.ChangeType.ADD)
                    {
                        git.Add().AddFilepattern(diff.GetNewPath()).Call();
                    }
                    else
                    {
                        git.Add().AddFilepattern(diff.GetOldPath()).Call();
                    }
                }

                var commit = git.Commit().SetCommitter(WebAppSettings.GitUserName, WebAppSettings.GitUserEmail)
                                         .SetMessage(commitMessage)
                                         .Call();
                return commit.Id.Name;
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Fetches all.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <returns>Return fetch result.</returns>
        public FetchResult FetchAll(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                var refSpecs = new RefSpec();
                refSpecs = refSpecs.SetForceUpdate(true);
                refSpecs = refSpecs.SetSourceDestination(Constants.R_HEADS + "*", Constants.R_REMOTES + "origin/*");

                var fetchCommand = git.Fetch();
                fetchCommand.SetRefSpecs(refSpecs);

                return fetchCommand.Call();
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Fetches all command.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        public void FetchAllCommand(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            var fetchArgument = "fetch -p";

            GitCommand(gitDirectory, fetchArgument);
        }

        /// <summary>
        /// Cleans the specified GIT directory.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        public void Clean(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                git.Clean().SetDryRun(true).Call();
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Pushes the specified GIT directory.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        public void Push(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                git.Push().SetForce(true).Call();
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Pushes the command.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        public void PushCommand(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            string pushArgument = "push origin master -f";

            GitCommand(gitDirectory, pushArgument);
        }

        /// <summary>
        /// Resets the specified GIT directory.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="commitHash">The commit hash.</param>
        public void Reset(string gitDirectory, string commitHash)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                git.Clean().Call();

                var resetCommand = git.Reset();
                resetCommand.SetMode(ResetCommand.ResetType.HARD);
                resetCommand.SetRef(commitHash);
                resetCommand.Call();
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Gets the commit.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="commitHash">The commit hash.</param>
        /// <returns>Return commit information.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Throw ArgumentOutOfRangeException if commit hash was not found.</exception>
        public RevCommit GetCommit(string gitDirectory, string commitHash)
        {
            gitDirectory = gitDirectory.Trim();
            var commit = TryGetCommit(gitDirectory, commitHash);
            if (commit == null)
            {
                throw new ArgumentException(commitHash);
            }

            return commit;
        }

        /// <summary>
        /// Gets all branches.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <returns>Return branch name list.</returns>
        public IList<Ref> GetAllBranches(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                return git.BranchList().Call();
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <returns>Return branch name list.</returns>
        public IList<Ref> GetAllTags(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                return git.TagList().Call();
            }
            finally
            {
                CloseGit(git);
            }
        }

        public bool ContainsHash(string gitDirectory, string commitHash)
        {
            gitDirectory = gitDirectory.Trim();
            var commit = TryGetCommit(gitDirectory, commitHash);
            return commit != null;
        }

        /// <summary>
        /// Gets the commit difference.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="oldCommitHash">The old commit hash.</param>
        /// <param name="newCommitHash">The new commit hash.</param>
        /// <returns>List off all differences.</returns>
        public IList<GitFileDiff> GetCommitDiff(string gitDirectory, string oldCommitHash, string newCommitHash)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);
            var repository = git.GetRepository();

            try
            {
                var diffEntries = git.Diff()
                       .SetShowNameAndStatusOnly(true)
                       .SetOldTree(GetTreeIterator(repository, oldCommitHash))
                       .SetNewTree(GetTreeIterator(repository, newCommitHash))
                       .Call();
                return diffEntries.Select(d => TransformDiffEntryToFileDiff(d)).ToList();
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Gets the commit file difference.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="oldCommitHash">The old commit hash.</param>
        /// <param name="newCommitHash">The new commit hash.</param>
        /// <returns>The different content.</returns>
        public string GetCommitFileDiff(string gitDirectory, string filePath, string oldCommitHash, string newCommitHash)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);
            var repository = git.GetRepository();
            MemoryStream memoryStream = null;
            SafeBufferedOutputStream outputStream = null;
            StreamReader streamReader = null;

            try
            {
                memoryStream = new MemoryStream();
                outputStream = new SafeBufferedOutputStream(memoryStream);
                git.Diff()
                    .SetOutputStream(outputStream)
                    .SetPathFilter(PathFilter.Create(filePath))
                    .SetOldTree(GetTreeIterator(repository, oldCommitHash))
                    .SetNewTree(GetTreeIterator(repository, newCommitHash))
                    .Call();

                memoryStream.Position = 0;
                streamReader = new StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                CloseReader(streamReader);
                CloseStream(outputStream);
                CloseStream(memoryStream);
                CloseGit(git);
            }
        }

        /// <summary>
        /// Get difference between working GIT directory and repository database.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <returns>List of different entry.</returns>
        public IList<DiffEntry> GetWorkingDiff(string gitDirectory)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                return git.Diff()
                       .SetShowNameAndStatusOnly(true)
                       .Call();
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// Get difference between a working file with repository database.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The different content.</returns>
        public string GetWorkingFileDiff(string gitDirectory, string filePath)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);
            MemoryStream memoryStream = null;
            SafeBufferedOutputStream outputStream = null;
            StreamReader streamReader = null;
            try
            {
                memoryStream = new MemoryStream();
                outputStream = new SafeBufferedOutputStream(memoryStream);

                git.Diff()
                    .SetOutputStream(outputStream)
                    .SetPathFilter(PathFilter.Create(filePath))
                    .Call();

                memoryStream.Position = 0;
                streamReader = new StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                CloseReader(streamReader);
                CloseStream(outputStream);
                CloseStream(memoryStream);
                CloseGit(git);
            }
        }

        /// <summary>
        /// Clones the repository.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="repositoryUrl">The repository URL.</param>
        public void CloneRepository(string directory, Uri repositoryUrl)
        {
            Git.CloneRepository()
                .SetDirectory(directory)
                .SetURI(repositoryUrl.AbsoluteUri)
                .Call();
        }

        /// <summary>
        /// Gets the changed files from specific commit.
        /// </summary>
        /// <param name="gitDirectory">The git directory.</param>
        /// <param name="commitHash">The commit hash.</param>
        /// <returns>The changed files from specific commit.</returns>
        public IList<GitFileDiff> GetChangedFilesFromSpecificCommit(string gitDirectory, string commitHash)
        {
            var result = new List<GitFileDiff>();
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                Repository repo = git.GetRepository();
                ObjectId lastCommitId = repo.Resolve(commitHash);
                RevWalk revWalk = new RevWalk(repo);
                RevCommit commit = revWalk.ParseCommit(lastCommitId);

                if (commit.ParentCount > 0)
                {
                    string parentCommitHash = commit.Parents[0].Name;

                    result = GetCommitDiff(gitDirectory, parentCommitHash, commitHash).ToList();
                }
                else
                {
                    result = GetChangedFilesFromFirstCommit(repo, commit);
                }

                revWalk.Dispose();
            }
            finally
            {
                CloseGit(git);
            }

            return result;
        }

        /// <summary>
        /// Gets the changed files from first commit.
        /// </summary>
        /// <param name="repo">The repository.</param>
        /// <param name="commit">The revision commit.</param>
        /// <returns>The changed files from first commit.</returns>
        private static List<GitFileDiff> GetChangedFilesFromFirstCommit(Repository repo, RevCommit commit)
        {
            List<GitFileDiff> result = new List<GitFileDiff>();

            TreeWalk treeWalk = new TreeWalk(repo);
            treeWalk.Reset();
            treeWalk.AddTree(commit.Tree);
            treeWalk.Recursive = true;

            while (treeWalk.Next())
            {
                var gitFile = new GitFileDiff
                {
                    FilePath = treeWalk.PathString
                };

                result.Add(gitFile);
            }

            treeWalk.Release();

            return result;
        }

        /// <summary>
        /// Gets the commit.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="commitHash">The commit hash.</param>
        /// <returns>If the commit hash is found, return commit information. Otherwise, return null.</returns>
        private static RevCommit TryGetCommit(string gitDirectory, string commitHash)
        {
            gitDirectory = gitDirectory.Trim();
            var workingDirectoryPath = new Sharpen.FilePath(gitDirectory);
            var git = Git.Open(workingDirectoryPath);

            try
            {
                var logs = git.Log().All().Call();
                foreach (var log in logs)
                {
                    if (log.Id.Name.Equals(commitHash))
                    {
                        return log;
                    }
                }

                return null;
            }
            finally
            {
                CloseGit(git);
            }
        }

        /// <summary>
        /// GetTreeIterator of a commit.
        /// </summary>
        /// <param name="repository">Repository object.</param>
        /// <param name="commitName">Commit name.</param>
        /// <returns>AbstractTreeIterator object.</returns>
        private static AbstractTreeIterator GetTreeIterator(Repository repository, string commitName)
        {
            commitName = string.IsNullOrWhiteSpace(commitName) ? GetRootCommitHash(repository) : commitName;
            var id = repository.Resolve(commitName);
            var parser = new CanonicalTreeParser();
            var objectReader = repository.NewObjectReader();
            try
            {
                parser.Reset(objectReader, new NGit.Revwalk.RevWalk(repository).ParseTree(id));
                return parser;
            }
            finally
            {
                objectReader.Release();
            }
        }

        /// <summary>
        /// Transforms the difference entry to file difference.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>GITFileDiff object.</returns>
        private static GitFileDiff TransformDiffEntryToFileDiff(DiffEntry entry)
        {
            var filePath = entry.GetOldPath();
            filePath = filePath.EndsWith("/dev/null", StringComparison.CurrentCultureIgnoreCase)
                ? entry.GetNewPath()
                : filePath;

            return new GitFileDiff
            {
                FilePath = filePath,
                ChangeType = entry.GetChangeType().ToString()
            };
        }

        /// <summary>
        /// Get root commit hash of repository.
        /// </summary>
        /// <param name="repository">Repository object.</param>
        /// <returns>Commit hash.</returns>
        private static string GetRootCommitHash(Repository repository)
        {
            var plotWalk = new PlotWalk(repository);
            var refsByPeeledList = repository.GetAllRefsByPeeledObjectId();
            var revCommitList = refsByPeeledList
                                .Keys
                                .Select(id => plotWalk.ParseCommit(id))
                                .ToList();

            plotWalk.MarkStart(revCommitList);

            var commitList = new PlotCommitList<PlotLane>();
            commitList.Source(plotWalk);
            commitList.FillTo(int.MaxValue);

            return commitList.Last().Name;
        }

        private static void CloseStream(Stream stream)
        {
            if (stream != null)
            {
                stream.Close();
            }
        }

        private static void CloseReader(TextReader reader)
        {
            if (reader != null)
            {
                reader.Close();
            }
        }

        private static void CloseGit(Git git)
        {
            git.GetRepository().Close();
            git.GetRepository().ObjectDatabase.Close();
        }

        /// <summary>
        /// GIT the command.
        /// </summary>
        /// <param name="gitDirectory">The GIT directory.</param>
        /// <param name="gitArguments">The GIT arguments.</param>       
        private static void GitCommand(string gitDirectory, string gitArguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(WebAppSettings.GitCommandFilePath);
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = gitDirectory;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = gitArguments;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}