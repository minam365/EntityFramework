// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Migrations
{
    public class MigrationCommandList
    {
        private readonly List<MigrationCommand> _migrationCommands;

        public MigrationCommandList([NotNull] IEnumerable<MigrationCommand> migrationCommands)
        {
            Check.NotNull(migrationCommands, nameof(migrationCommands));

            _migrationCommands = migrationCommands.ToList();
        }

        public virtual IReadOnlyList<MigrationCommand> MigrationCommands => _migrationCommands;

        public virtual void ExecuteNonQuery([NotNull] IRelationalConnection connection)
        {
            Check.NotNull(connection, nameof(connection));

            connection.Open();

            try
            {
                IDbContextTransaction transaction = null;

                try
                {
                    foreach (var command in _migrationCommands)
                    {
                        if (transaction == null
                            && !command.TransactionSuppressed)
                        {
                            transaction = connection.BeginTransaction();
                        }

                        if (transaction != null
                            && command.TransactionSuppressed)
                        {
                            transaction.Commit();
                            transaction.Dispose();
                            transaction = null;
                        }

                        command.RelationalCommand.ExecuteNonQuery(connection);
                    }

                    transaction?.Commit();
                }
                finally
                {
                    transaction?.Dispose();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public virtual async Task ExecuteNonQueryAsync(
            [NotNull] IRelationalConnection connection,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Check.NotNull(connection, nameof(connection));

            await connection.OpenAsync(cancellationToken);

            try
            {
                IDbContextTransaction transaction = null;

                try
                {
                    foreach (var command in _migrationCommands)
                    {
                        if (transaction == null
                            && !command.TransactionSuppressed)
                        {
                            transaction = await connection.BeginTransactionAsync(cancellationToken);
                        }

                        if (transaction != null
                            && command.TransactionSuppressed)
                        {
                            transaction.Commit();
                            transaction.Dispose();
                            transaction = null;
                        }

                        await command.RelationalCommand.ExecuteNonQueryAsync(connection, cancellationToken: cancellationToken);
                    }

                    transaction?.Commit();
                }
                finally
                {
                    transaction?.Dispose();
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
