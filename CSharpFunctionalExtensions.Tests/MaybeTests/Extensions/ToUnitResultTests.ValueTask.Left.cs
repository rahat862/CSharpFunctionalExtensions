﻿using System.Threading.Tasks;
using CSharpFunctionalExtensions.ValueTasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.MaybeTests.Extensions
{
    public class ToUnitResultTests_ValueTask_Left : MaybeTestBase
    {
        [Fact]
        public async Task ToUnitResult_ValueTask_Left_returns_failure_if_has_error_value()
        {
            var maybe = Maybe<E>.From(E.Value);

            var result = await maybe.AsValueTask().ToUnitResult();

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task ToUnitResult_ValueTask_Left_returns_success_if_has_no_error_value()
        {
            Maybe<E> maybe = null;

            var result = await maybe.AsValueTask().ToUnitResult();

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ToUnitResult_ValueTask_Left_returns_failure_if_has_no_value()
        {
            Maybe<T> maybe = null;

            var result = await maybe.AsValueTask().ToUnitResult("Error");

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Error");
        }

        [Fact]
        public async Task ToUnitResult_ValueTask_Left_returns_success_if_has_value()
        {
            var maybe = Maybe<T>.From(T.Value);

            var result = await maybe.AsValueTask().ToUnitResult("Error");

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ToUnitResult_ValueTask_Left_returns_custom_failure_if_has_no_value()
        {
            Maybe<T> maybe = null;

            var result = await maybe.AsValueTask().ToUnitResult(E.Value);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(E.Value);
        }

        [Fact]
        public async Task ToUnitResult_ValueTask_Left_custom_failure_returns_success_if_has_value()
        {
            var maybe = Maybe<T>.From(T.Value);

            var result = await maybe.AsValueTask().ToUnitResult(E.Value);

            result.IsSuccess.Should().BeTrue();
        }
    }
}
