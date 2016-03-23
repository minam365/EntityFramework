// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Migrations
{
    public class MigrationCommand
    {
        public MigrationCommand(
            [NotNull] IRelationalCommand relationalCommand,
            bool transactionSuppressed)
        {
            Check.NotNull(relationalCommand, nameof(relationalCommand));

            RelationalCommand = relationalCommand;
            TransactionSuppressed = transactionSuppressed;
        }

        public virtual IRelationalCommand RelationalCommand { get; }
        public virtual bool TransactionSuppressed { get; }
    }
}
