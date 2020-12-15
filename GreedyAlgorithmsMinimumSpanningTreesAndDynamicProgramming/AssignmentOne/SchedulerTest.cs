﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utility.Common;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.AssignmentOne
{
    public class SchedulerTest
    {
        private readonly double[][] _jobs =
        {
            new[] {8.0, 50.0},
            new[] {74.0, 59.0},
            new[] {31.0, 73.0},
            new[] {45.0, 79.0}
        };

        [Test]
        public void GivenJobs_ShouldSortByWeightMinusLengthAndWeightFirstIfTie()
        {
            var sut = new Scheduler();
            var sorted = sut.SortByWeightMinusLength(_jobs);
            Assert.AreEqual(new List<List<double>>
            {
                new() {31, 73, -42, 73},
                new() {8, 50, -42, 73+50},
                new() {45, 79, -34, 73+50+79},
                new() {74, 59, 15, 73+50+79+59}
            }, sorted);
        }

        [Test]
        public void GivenJobs_ShouldReturnWeightedCompletionTime()
        {
            var sut = new Scheduler();
            var weightedCompletionTime = sut.GetWeightedCompletionTime(sut.SortByWeightMinusLength(_jobs));
            Assert.AreEqual(31651, weightedCompletionTime);
        }

        [Test]
        public void GivenAssignmentJobs_ShouldReturnWeightedCompletedTime()
        {
            var sut = new Scheduler();
            var jobs = new FileReader().ReadFile("AssignmentOne", "jobs.txt")
                .Select(x => x.Split(" ").Select(double.Parse).ToArray()).Skip(1).ToArray();
            var weightedCompletionTime = sut.GetWeightedCompletionTime(sut.SortByWeightMinusLength(jobs));

            Assert.AreEqual(188635738448, weightedCompletionTime);
        }

        [Test]
        public void GivenJobs_ShouldReturnSortedJobsByWeightLengthRatio()
        {
            var sut = new Scheduler();
            var sorted = sut.SortByWeightLengthRatio(_jobs.Select(x => x.Select(Convert.ToDouble).ToArray()).ToArray());
            Assert.AreEqual(new List<List<double>>
            {
                new() {74, 59, 74.0 / 59.0, 59}, //1.25
                new() {45, 79, 45.0 / 79.0, 59 + 79}, //0.56
                new() {31, 73, 31.0 / 73.0, 59 + 79 + 73}, //0.42
                new() {8, 50, 8.0 / 50.0, 59 + 79 + 73 + 50}, //0.16
            }, sorted);
        }

        [Test]
        public void GivenAssignmentJobs_ShouldReturnCount()
        {
            var sut = new Scheduler();
            var jobs = new FileReader().ReadFile("AssignmentOne", "jobs.txt")
                .Select(x => x.Split(" ").Select(double.Parse).ToArray()).Skip(1).ToArray();
            var weightedCompletionTime = sut.GetWeightedCompletionTime(sut.SortByWeightLengthRatio(jobs));

            Assert.AreEqual(67311454237, weightedCompletionTime);
        }
    }
}