using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PROG_POE.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PROG_POE.Controllers
{
    public class ServiceRequestController : Controller
    {
        // initializing graph and tree
        private readonly ServiceRequestGraph graph = new();
        private readonly ServiceRequestTree tree = new();
        public ServiceRequestController()
        {
            // inputting dummy data into tree
            tree.root = new Node { data = new ServiceRequest { Description = "All Service Requests" } };
            tree.root.Children = new List<Node>
            {
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 1,
                        Description = "Fixes for Admirality Road",
                        Status = "In Progress",
                        SubmissionDate = DateTime.Now.Date.AddDays(-6),
                        Priority = 2
                    }
                },
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 2,
                        Description = "Beach Maintenance",
                        Status = "Pending",
                        SubmissionDate = DateTime.Now.Date.AddDays(-3),
                        Priority = 1
                    }
                },
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 3,
                        Description = "Solve Power Outage",
                        Status = "Complete",
                        SubmissionDate = DateTime.Now.Date.AddDays(-9),
                        Priority = 3
                    }
                }
            };

            tree.root.Children[0].Children = new List<Node>
            {
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 11,
                        Description = "Fix streetlight",
                        Status = "In Progress",
                        SubmissionDate = DateTime.Now.Date.AddDays(-2),
                        Priority = 1
                    }
                },
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 12,
                        Description = "Fix pothole",
                        Status = "In Progress",
                        SubmissionDate = DateTime.Now.Date.AddDays(-4),
                        Priority = 2
                    }
                },
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 13,
                        Description = "Fix robot",
                        Status = "Complete",
                        SubmissionDate = DateTime.Now.Date.AddDays(-6),
                        Priority = 3
                    }
                }
            };

            tree.root.Children[1].Children = new List<Node>
            {
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 21,
                        Description = "Clean up beach",
                        Status = "Pending",
                        SubmissionDate = DateTime.Now.Date.AddDays(-4),
                        Priority = 1
                    }
                },
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 22,
                        Description = "Fix beach parking",
                        Status = "Complete",
                        SubmissionDate = DateTime.Now.Date.AddDays(-3),
                        Priority = 2
                    }
                }
            };

            tree.root.Children[2].Children = new List<Node>
            {
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 31,
                        Description = "Fix broken breaker",
                        Status = "Complete",
                        SubmissionDate = DateTime.Now.Date.AddDays(-9),
                        Priority = 3
                    }
                },
                new Node
                {
                    data = new ServiceRequest
                    {
                        RequestId = 32,
                        Description = "Fix cables",
                        Status = "Complete",
                        SubmissionDate = DateTime.Now.Date.AddDays(-8),
                        Priority = 3
                    }
                }
            };

            // get list of all nodes from tree
            var requestList = tree.GetList();

            // add each request to graph
            foreach (var request in requestList)
            {
                graph.AddRequest(request.data.RequestId, request);
            }

            // saves dependancy in graph between parent and child nodes
            foreach (var request in graph.requestList)
            {
                if(request.Value.Data.Children.Count > 0)
                {
                    foreach(var child in request.Value.Data.Children)
                    {
                        graph.addEdges(request.Value.Data.data.RequestId, child.data.RequestId);
                    }
                }
            }
        }
        // shows list of all service requests
        public ActionResult Index()
        {
            //makes report list for session on sign in 
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }
            var nodes = tree.GetList();
            return View(nodes);
        }

        // shows the dependancy for selected service request
        // it shows which services requests need to be completed before this service request can be completed
        public ActionResult Dependencies(int requestId)
        {
            //makes report list for session on sign in 
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }
            // finds node
            var node = tree.GetList().FirstOrDefault(x=> x.data.RequestId == requestId);

            // gets nodes dependancies
            var dependencies = graph.GetDependencies(requestId);

            // passes both the node and its dependancies to view
            DependencyPage variables = new DependencyPage();
            variables.nodes = dependencies;
            variables.node = node;
            return View(variables);
        }

        // searches for service requests based on user input
        [HttpGet]
        public IActionResult Index(string searchString, string priority, string status, DateTime? requestDate)
        {
            //makes report list for session on sign in 
            if (HttpContext.Session.GetString("SessionUser") == "")
            {
                return RedirectToAction("SignIn");
            }
            RequestSearch search = new RequestSearch();
            // returns full list if nothing is selected
            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(priority) && string.IsNullOrEmpty(status) && !requestDate.HasValue)
            {
                var nodes = tree.GetList();
                return View(nodes);
            }

            // adds information the the request search
            if (searchString != null)
            {
                search.id = searchString;
            }
            if (priority != null)
            {
                search.priority = priority;
            }
            if (status != null)
            {
                search.status = status;
            }
            if (requestDate.HasValue)
            {
                search.date = requestDate;
            }

            // gets found service requests from tree and displays it in list
            var found = tree.FoundList(search);

            return View(found);
        }
    }
}
