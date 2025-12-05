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
                //Display menu options for user interaction with colors for better readability.
                Console.ForegroundColor = ConsoleColor.Cyan; //Set menu header color.
                Console.WriteLine("\n--- SagePKM Menu ---");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green; //Set menu options color.
                Console.WriteLine("1. Add a new node");
                Console.WriteLine("2. View all nodes");
                Console.WriteLine("3. Search nodes by tag");
                Console.WriteLine("4. Exit");
                Console.ResetColor();

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

                        //Feedback with emoji for engagement.
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✅ Node added successfully!");
                        Console.ResetColor();
                        break;

                    case "2":
                        //Display all nodes currently in the knowledge graph in structured format.
                        Console.WriteLine($"\n📚 Knowledge Graph contains {graph.Nodes.Count} node(s):");

                        //Loop through each node and print in structured format with vertical spacing.
                        int count = 1;
                        foreach (var n in graph.Nodes)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine($"Node {count}:");
                            Console.ResetColor();

                            //Print title
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"Title    : {n.Title}");
                            Console.ResetColor();

                            //Print summary
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"Summary  : {n.GetSummary()}");
                            Console.ResetColor();

                            //Print tags
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Tags     : {string.Join(", ", n.Tags)}");
                            Console.ResetColor();

                            //Separator line for visual clarity between nodes.
                            Console.WriteLine("--------------------------------------------------------------------------------");
                            count++;
                        }
                        break;

                    case "3":
                        //Prompt user for tag keyword and search nodes.
                        Console.Write("Enter tag to search: ");
                        var keyword = Console.ReadLine();
                        var results = user.SearchNodes(graph, keyword);

                        //Display search results in structured format.
                        Console.WriteLine($"\n🔎 Found {results.Count} node(s) with tag '{keyword}':");

                        //Loop through each result and print in structured format with vertical spacing.
                        int resultCount = 1;
                        foreach (var result in results)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine($"Node {resultCount}:");
                            Console.ResetColor();

                            //Print title
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"Title    : {result.Title}");
                            Console.ResetColor();

                            //Print summary
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"Summary  : {result.GetSummary()}");
                            Console.ResetColor();

                            //Print tags
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Tags     : {string.Join(", ", result.Tags)}");
                            Console.ResetColor();

                            //Separator line for visual clarity between nodes.
                            Console.WriteLine("--------------------------------------------------------------------------------");
                            resultCount++;
                        }
                        break;

                    case "4":
                        //Exit the program when user chooses option 4.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("👋 Exiting SagePKM. Goodbye!");
                        Console.ResetColor();
                        return;

                    default:
                        //Handle invalid menu choice.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Try again.");
                        Console.ResetColor();
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
        //Now case-insensitive to improve usability.
        public List<Node> SearchNodes(KnowledgeGraph graph, string keyword)
        {
            return graph.Nodes.FindAll(n =>
                n.Tags.Exists(t => t.Equals(keyword, StringComparison.OrdinalIgnoreCase)));
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
