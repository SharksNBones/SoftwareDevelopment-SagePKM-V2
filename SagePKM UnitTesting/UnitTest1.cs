using Xunit;
using Program; // Your main namespace
using System.Collections.Generic;

namespace SagePKM.UnitTesting
{
    public class SagePKMTests
    {
        [Fact] //Marks this method as a test case in xUnit
        public void GetSummary_Should_Truncate_Long_Content()
        {
            var node = new Node("Test", new string('A', 60), new List<string> { "tag1" });
            var summary = node.GetSummary();

            Assert.EndsWith("...", summary);
            Assert.Equal(53, summary.Length); //50 chars + "..."
        }

        [Fact]
        public void AddNode_Should_Store_Node_In_Graph()
        {
            var graph = new KnowledgeGraph();
            var node = new Node("Title", "Content", new List<string> { "tag" });

            graph.AddNode(node);

            Assert.Single(graph.Nodes);
            Assert.Equal("Title", graph.Nodes[0].Title);
        }

        [Fact]
        public void SearchNodes_Should_Be_CaseInsensitive()
        {
            var user = new User(1, "Alice", "Researcher");
            var graph = new KnowledgeGraph();
            var node = user.CreateNode("Java Tutorial", "Content", new List<string> { "Java" });
            graph.AddNode(node);

            var results = user.SearchNodes(graph, "java");

            Assert.Single(results);
            Assert.Equal("Java Tutorial", results[0].Title);
        }

        [Fact]
        public void NodeAdd_Should_Combine_Content()
        {
            var node1 = new Node("Combined", "First. ", new List<string> { "merge" });
            var node2 = new Node("Combined", "Second.", new List<string> { "merge" });

            var combined = node1.add(node2);

            Assert.Contains("First. ", combined.Content);
            Assert.Contains("Second.", combined.Content);
        }
    }
}
