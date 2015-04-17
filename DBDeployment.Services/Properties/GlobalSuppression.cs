// --------------------------------------------------------------------------
// <copyright file="GlobalSuppression.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.4.17">Created</change>
// </history>

using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage(
    "Microsoft.Security",
    "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands",
    Scope = "member",
    Target = "DBDeployment.Services.Git.GitService.#GitCommand(System.String,System.String)",
    Justification = "Reviewed. Suppression is OK here.")]