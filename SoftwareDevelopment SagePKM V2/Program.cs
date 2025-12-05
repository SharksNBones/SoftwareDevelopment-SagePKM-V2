using System;
using System.Collections.Generic;

namespace Program
{
    //Start of program. 
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create a new user with ID=1, name "Alice", and role "Researcher".
            var user = new User(1, "Alice", "Researcher");

            //Create a new empty knowledge graph.
            var graph = new KnowledgeGraph();

            //Loop to continuously show menu until user exits.
            while (true)
            {
                //Display menu options for user interaction.
                Console.WriteLine("\n--- SagePKM Menu ---");
                Console.WriteLine("1. Add a new node");
                Console.WriteLine("2. View all nodes");
                Console.WriteLine("3. Search nodes by tag");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //Prompt user for node details (title, content, tags).
                        Console.Write("Enter node title: ");
                        var title = Console.ReadLine();

                        Console.Write("Enter node content: ");
                        var content = Console.ReadLine();

                        Console.Write("Enter tags (comma separated): ");
                        var tagsInput = Console.ReadLine();
                        var tags = new List<string>(tagsInput.Split(',', StringSplitOptions.RemoveEmptyEntries));

                        //Create and add node to the knowledge graph.
                        var node = user.CreateNode(title, content, tags);
                        graph.AddNode(node);

                        Console.WriteLine("✅ Node added successfully!");
                        break;

                    case "2":
                        //Display all nodes currently in the knowledge graph in table format.
                        Console.WriteLine($"\nKnowledge Graph contains {graph.Nodes.Count} node(s):");

                        //Print table header.
                        Console.WriteLine("------------------------------------------------------------");
                        Console.WriteLine($"{"Title",-20} {"Summary",-30} {"Tags",-20}");
                        Console.WriteLine("------------------------------------------------------------");

                        //Loop through each node and print in table format.
                        foreach (var n in graph.Nodes)
                        {
                            Console.WriteLine($"{n.Title,-20} {n.GetSummary(),-30} {string.Join(", ", n.Tags),-20}");
                        }
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case "3":
                        //Prompt user for tag keyword and search nodes.
                        Console.Write("Enter tag to search: ");
                        var keyword = Console.ReadLine();
                        var results = user.SearchNodes(graph, keyword);

                        //Display search results in table format.
                        Console.WriteLine($"\nFound {results.Count} node(s) with tag '{keyword}':");
                        Console.WriteLine("------------------------------------------------------------");
                        Console.WriteLine($"{"Title",-20} {"Summary",-30} {"Tags",-20}");
                        Console.WriteLine("------------------------------------------------------------");

                        foreach (var result in results)
                        {
                            Console.WriteLine($"{result.Title,-20} {result.GetSummary(),-30} {string.Join(", ", result.Tags),-20}");
                        }
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case "4":
                        //Exit the program when user chooses option 4.
                        Console.WriteLine("👋 Exiting SagePKM. Goodbye!");
                        return;

                    default:
                        //Handle invalid menu choice.
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }

    // Represents a user who interacts with the knowledge graph.
    public class User
    {
        public int UserId { get; } //Unique identifier for the user.
        public string Name { get; } //User's name.
        public string Role { get; } //user's role (e.g., Researcher, Admin).

        // Constructor to initialize a new user with ID, name, and role.
        public User(int userId, string name, string role)
        {
            UserId = userId;
            Name = name;
            Role = role;
        }

        //Method for creating a new node with given title, content, and tags in the knowledge graph.
        public Node CreateNode(string title, string content, List<string> tags)
        {
            return new Node(title, content, tags);
        }

        //Method for searching nodes in the knowledge graph that contain a specific keyword in their tags.
        public List<Node> SearchNodes(KnowledgeGraph graph, string keyword)
        {
            return graph.Nodes.FindAll(n => n.Tags.Contains(keyword));
        }
    }

    // Represents a knowledge graph containing multiple nodes.
    public class KnowledgeGraph
    {
        // List of all nodes in the knowledge graph.
        public List<Node> Nodes { get; } = new List<Node>();

        //Method to add a new node to the knowledge graph.
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
    }

    // Represents a single node in the knowledge graph.
    public class Node
    {
        public string Title { get; } // Title of the node.
        public string Content { get; } // Full content of the node.
        public List<string> Tags { get; } // Tags for categorization/Search.

        // Constructor to initialize a node
        public Node(string title, string content, List<string> tags)
        {
            Title = title;
            Content = content;
            Tags = tags;
        }

        // Returns a short summary of the content (first 50 chars)
        public string GetSummary()
        {
            return Content.Length > 50 ? Content.Substring(0, 50) + "..." : Content;
        }

        // Combines this node's content with another node's content
        // Returns a new node with merged content but same title/tags
        public Node add(Node other)
        {
            return new Node(Title, Content + other.Content, Tags);
        }
    }
}
