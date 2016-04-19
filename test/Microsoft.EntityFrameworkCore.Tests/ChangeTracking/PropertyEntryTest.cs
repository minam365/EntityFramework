// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Tests.ChangeTracking
{
    public class PropertyEntryTest
    {
        [Fact]
        public void Can_get_name()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new Wotty { Id = 1, Primate = "Monkey" });

            Assert.Equal("Primate", new PropertyEntry(entry, "Primate").Metadata.Name);
        }

        [Fact]
        public void Can_get_current_value()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new Wotty { Id = 1, Primate = "Monkey" });

            Assert.Equal("Monkey", new PropertyEntry(entry, "Primate").CurrentValue);
        }

        [Fact]
        public void Can_set_current_value()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            new PropertyEntry(entry, "Primate").CurrentValue = "Chimp";

            Assert.Equal("Chimp", entity.Primate);
        }

        [Fact]
        public void Can_set_current_value_to_null()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            new PropertyEntry(entry, "Primate").CurrentValue = null;

            Assert.Null(entity.Primate);
        }

        [Fact]
        public void Can_set_and_get_original_value()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry(entry, "Primate").OriginalValue);

            new PropertyEntry(entry, "Primate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry(entry, "Primate").OriginalValue);
            Assert.Equal("Monkey", entity.Primate);
        }

        [Fact]
        public void Can_set_original_value_to_null()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new Wotty { Id = 1, Primate = "Monkey" });

            new PropertyEntry(entry, "Primate").OriginalValue = null;

            Assert.Null(new PropertyEntry(entry, "Primate").OriginalValue);
        }

        [Fact]
        public void Can_set_and_clear_modified()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.False(new PropertyEntry(entry, "Primate").IsModified);

            new PropertyEntry(entry, "Primate").IsModified = true;

            Assert.True(new PropertyEntry(entry, "Primate").IsModified);

            new PropertyEntry(entry, "Primate").IsModified = false;

            Assert.False(new PropertyEntry(entry, "Primate").IsModified);
        }

        [Fact]
        public void Can_get_name_generic()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new Wotty { Id = 1, Primate = "Monkey" });

            Assert.Equal("Primate", new PropertyEntry<Wotty, string>(entry, "Primate").Metadata.Name);
        }

        [Fact]
        public void Can_get_current_value_generic()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new Wotty { Id = 1, Primate = "Monkey" });

            Assert.Equal("Monkey", new PropertyEntry<Wotty, string>(entry, "Primate").CurrentValue);
        }

        [Fact]
        public void Can_set_current_value_generic()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            new PropertyEntry<Wotty, string>(entry, "Primate").CurrentValue = "Chimp";

            Assert.Equal("Chimp", entity.Primate);
        }

        [Fact]
        public void Can_set_current_value_to_null_generic()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            new PropertyEntry<Wotty, string>(entry, "Primate").CurrentValue = null;

            Assert.Null(entity.Primate);
        }

        [Fact]
        public void Can_set_and_get_original_value_generic()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry<Wotty, string>(entry, "Primate").OriginalValue);

            new PropertyEntry<Wotty, string>(entry, "Primate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry<Wotty, string>(entry, "Primate").OriginalValue);
            Assert.Equal("Monkey", entity.Primate);
        }

        [Fact]
        public void Can_set_original_value_to_null_generic()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new Wotty { Id = 1, Primate = "Monkey" });

            new PropertyEntry<Wotty, string>(entry, "Primate").OriginalValue = null;

            Assert.Null(new PropertyEntry<Wotty, string>(entry, "Primate").OriginalValue);
        }

        [Fact]
        public void Can_set_and_clear_modified_generic()
        {
            var entity = new Wotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.False(new PropertyEntry<Wotty, string>(entry, "Primate").IsModified);

            new PropertyEntry(entry, "Primate").IsModified = true;

            Assert.True(new PropertyEntry<Wotty, string>(entry, "Primate").IsModified);

            new PropertyEntry(entry, "Primate").IsModified = false;

            Assert.False(new PropertyEntry<Wotty, string>(entry, "Primate").IsModified);
        }

        [Fact]
        public void Can_set_and_get_original_value_notifying_entities()
        {
            var entity = new NotifyingWotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry(entry, "Primate").OriginalValue);

            new PropertyEntry(entry, "Primate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry(entry, "Primate").OriginalValue);
            Assert.Equal("Monkey", entity.Primate);
        }

        [Fact]
        public void Can_set_original_value_to_null_notifying_entities()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new NotifyingWotty { Id = 1, Primate = "Monkey" });

            new PropertyEntry(entry, "Primate").OriginalValue = null;

            Assert.Null(new PropertyEntry(entry, "Primate").OriginalValue);
        }

        [Fact]
        public void Can_set_and_get_original_value_generic_notifying_entities()
        {
            var entity = new NotifyingWotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry<NotifyingWotty, string>(entry, "Primate").OriginalValue);

            new PropertyEntry<NotifyingWotty, string>(entry, "Primate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry<NotifyingWotty, string>(entry, "Primate").OriginalValue);
            Assert.Equal("Monkey", entity.Primate);
        }

        [Fact]
        public void Can_set_original_value_to_null_generic_notifying_entities()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new NotifyingWotty { Id = 1, Primate = "Monkey" });

            new PropertyEntry<NotifyingWotty, string>(entry, "Primate").OriginalValue = null;

            Assert.Null(new PropertyEntry<NotifyingWotty, string>(entry, "Primate").OriginalValue);
        }

        [Fact]
        public void Can_set_and_get_concurrency_token_original_value_full_notification_entities()
        {
            var entity = new FullyNotifyingWotty { Id = 1, ConcurrentPrimate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry(entry, "ConcurrentPrimate").OriginalValue);

            new PropertyEntry(entry, "ConcurrentPrimate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry(entry, "ConcurrentPrimate").OriginalValue);
            Assert.Equal("Monkey", entity.ConcurrentPrimate);
        }

        [Fact]
        public void Can_set_concurrency_token_original_value_to_null_full_notification_entities()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new FullyNotifyingWotty { Id = 1, ConcurrentPrimate = "Monkey" });

            new PropertyEntry(entry, "ConcurrentPrimate").OriginalValue = null;

            Assert.Null(new PropertyEntry(entry, "ConcurrentPrimate").OriginalValue);
        }

        [Fact]
        public void Can_set_and_get_concurrency_token_original_value_generic_full_notification_entities()
        {
            var entity = new FullyNotifyingWotty { Id = 1, ConcurrentPrimate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry<FullyNotifyingWotty, string>(entry, "ConcurrentPrimate").OriginalValue);

            new PropertyEntry<FullyNotifyingWotty, string>(entry, "ConcurrentPrimate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry<FullyNotifyingWotty, string>(entry, "ConcurrentPrimate").OriginalValue);
            Assert.Equal("Monkey", entity.ConcurrentPrimate);
        }

        [Fact]
        public void Can_set_concurrency_token_original_value_to_null_generic_full_notification_entities()
        {
            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                new FullyNotifyingWotty { Id = 1, ConcurrentPrimate = "Monkey" });

            new PropertyEntry<FullyNotifyingWotty, string>(entry, "ConcurrentPrimate").OriginalValue = null;

            Assert.Null(new PropertyEntry<FullyNotifyingWotty, string>(entry, "ConcurrentPrimate").OriginalValue);
        }

        [Fact]
        public void Cannot_set_or_get_original_value_when_not_tracked()
        {
            var entity = new FullyNotifyingWotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            var propertyEntry = new PropertyEntry(entry, "Primate");

            Assert.Equal(
                CoreStrings.OriginalValueNotTracked("Primate", "FullyNotifyingWotty"),
                Assert.Throws<InvalidOperationException>(() => propertyEntry.OriginalValue).Message);

            Assert.Equal(
                CoreStrings.OriginalValueNotTracked("Primate", "FullyNotifyingWotty"),
                Assert.Throws<InvalidOperationException>(() => propertyEntry.OriginalValue = "Chimp").Message);
        }

        [Fact]
        public void Cannot_set_or_get_original_value_when_not_tracked_generic()
        {
            var entity = new FullyNotifyingWotty { Id = 1, ConcurrentPrimate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(),
                EntityState.Unchanged,
                entity);

            var propertyEntry = new PropertyEntry<FullyNotifyingWotty, string>(entry, "Primate");

            Assert.Equal(
                CoreStrings.OriginalValueNotTracked("Primate", "FullyNotifyingWotty"),
                Assert.Throws<InvalidOperationException>(() => propertyEntry.OriginalValue).Message);

            Assert.Equal(
                CoreStrings.OriginalValueNotTracked("Primate", "FullyNotifyingWotty"),
                Assert.Throws<InvalidOperationException>(() => propertyEntry.OriginalValue = "Chimp").Message);
        }

        [Fact]
        public void Can_set_or_get_original_value_when_property_explicitly_marked_to_be_tracked()
        {
            var entity = new FullyNotifyingWotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(useEagerSnapshots: true),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry(entry, "Primate").OriginalValue);

            new PropertyEntry(entry, "Primate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry(entry, "Primate").OriginalValue);
            Assert.Equal("Monkey", entity.Primate);
        }

        [Fact]
        public void Can_set_or_get_original_value_when_property_explicitly_marked_to_be_tracked_generic()
        {
            var entity = new FullyNotifyingWotty { Id = 1, Primate = "Monkey" };

            var entry = TestHelpers.Instance.CreateInternalEntry(
                BuildModel(useEagerSnapshots: true),
                EntityState.Unchanged,
                entity);

            Assert.Equal("Monkey", new PropertyEntry<FullyNotifyingWotty, string>(entry, "Primate").OriginalValue);

            new PropertyEntry<FullyNotifyingWotty, string>(entry, "Primate").OriginalValue = "Chimp";

            Assert.Equal("Chimp", new PropertyEntry<FullyNotifyingWotty, string>(entry, "Primate").OriginalValue);
            Assert.Equal("Monkey", entity.Primate);
        }

        private class Wotty
        {
            public int Id { get; set; }
            public string Primate { get; set; }
        }

        private class FullyNotifyingWotty : HasChangedAndChanging
        {
            private int _id;
            private string _primate;
            private string _concurrentprimate;

            public int Id
            {
                get { return _id; }
                set
                {
                    if (_id != value)
                    {
                        OnPropertyChanging();
                        _id = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Primate
            {
                get { return _primate; }
                set
                {
                    if (_primate != value)
                    {
                        OnPropertyChanging();
                        _primate = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string ConcurrentPrimate
            {
                get { return _concurrentprimate; }
                set
                {
                    if (_concurrentprimate != value)
                    {
                        OnPropertyChanging();
                        _concurrentprimate = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        private class NotifyingWotty : HasChanged
        {
            private int _id;
            private string _primate;

            public int Id
            {
                get { return _id; }
                set
                {
                    if (_id != value)
                    {
                        _id = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Primate
            {
                get { return _primate; }
                set
                {
                    if (_primate != value)
                    {
                        _primate = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        private abstract class HasChanged : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName]string propertyName = "") 
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private abstract class HasChangedAndChanging : HasChanged, INotifyPropertyChanging
        {
            public event PropertyChangingEventHandler PropertyChanging;

            protected void OnPropertyChanging([CallerMemberName]string propertyName = "") 
                => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public IMutableModel BuildModel(bool useEagerSnapshots = false)
        {
            var builder = TestHelpers.Instance.CreateConventionBuilder();

            builder.Entity<Wotty>();
            builder.Entity<NotifyingWotty>();

            var wottyBuilder = builder.Entity<FullyNotifyingWotty>();
            wottyBuilder.Property(e => e.ConcurrentPrimate).IsConcurrencyToken();

            if (useEagerSnapshots)
            {
                wottyBuilder.Metadata.UseEagerSnapshots(true);
            }

            return builder.Model;
        }
    }
}
