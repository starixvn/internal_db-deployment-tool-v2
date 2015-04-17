// --------------------------------------------------------------------------
// <copyright file="IGitService.cs" company="Nexcel Solutions Vietnam">
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
    using DBDeployment.Models.Gits;
    using NGit;
    using NGit.Diff;
    using NGit.Revwalk;
    using NGit.Transport;

    public interface IGitService
    {
        string Commit(string gitDirectory, string commitMessage);

        bool ContainsHash(string gitDirectory, string commitHash);

        FetchResult FetchAll(string gitDirectory);

        void FetchAllCommand(string gitDirectory);

        IList<Ref> GetAllBranches(string gitDirectory);

        IList<Ref> GetAllTags(string gitDirectory);

        RevCommit GetCommit(string gitDirectory, string commitHash);

        IList<GitFileDiff> GetCommitDiff(string gitDirectory, string oldCommitHash, string newCommitHash);

        string GetCommitFileDiff(string gitDirectory, string filePath, string oldCommitHash, string newCommitHash);

        IList<DiffEntry> GetWorkingDiff(string gitDirectory);

        string GetWorkingFileDiff(string gitDirectory, string filePath);

        void Push(string gitDirectory);

        void PushCommand(string gitDirectory);

        void Reset(string gitDirectory, string commitHash);

        void Clean(string gitDirectory);

        void CloneRepository(string directory, Uri repositoryUrl);

        IList<GitFileDiff> GetChangedFilesFromSpecificCommit(string gitDirectory, string commitHash);
    }
}
