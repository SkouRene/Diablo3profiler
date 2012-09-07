using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using umbraco.cms.businesslogic.web;
using umbraco.NodeFactory;
using umbraco.interfaces;

namespace D3pCom.Utilities
{
    /// <summary>
    /// Utilities to use with Umbraco and related.
    /// </summary>
    public class UmbracoUtilities
    {
        private UmbracoUtilities()
        {
        }

        /// <summary>
        /// Will return collection of node ids from MultiNodeTreePcicker content.
        /// Time: O(n), Space: O(n), Where n - number of node ids.
        /// </summary>
        /// <param name="xmlValue"></param>
        /// <returns></returns>
        public static ISet<int> GetNodeIdsFromMultiNodeTreePicker(string xmlValue)
        {
            //<relatedTasks>
            //    <MultiNodePicker>
            //        <nodeId>1093</nodeId>
            //    </MultiNodePicker>
            //</relatedTasks>
            HashSet<int> result = new HashSet<int>();
            if(xmlValue != null && xmlValue != string.Empty)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlValue);
                var rootElement = doc.DocumentElement;
                if(rootElement.ChildNodes.Count > 0 && rootElement.Name == "MultiNodePicker")
                {
                    foreach(XmlNode nodeId in rootElement)
                    {
                        if(nodeId.Name == "nodeId")
                        {
                            int nodeIdParsedInteger;
                            if(Int32.TryParse(nodeId.InnerText, out nodeIdParsedInteger))
                            {
                                result.Add(nodeIdParsedInteger);
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Will build xml from collection of node ids to be stored as MultiNodeTreePicker content.
        /// Time: O(n), Space: O(n), Where n - number of node ids
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public static string BuildMultiNodePickerContent(ICollection<int> nodeIds)
        {
            StringBuilder result = new StringBuilder();
            if(nodeIds.Count > 0)
            {
                result.Append("<MultiNodePicker>");
                foreach(int nodeId in nodeIds)
                {
                    result.Append("<nodeId>" + nodeId + "</nodeId>");
                }
                result.Append("</MultiNodePicker>");
            }
            return result.ToString();
        }

        /// <summary>
        /// Will fetech  all documents specified on collection of document ids.
        /// Time: O(n), Space: O(n), Where n - number of documents.
        /// </summary>
        /// <param name="documentIds"></param>
        /// <returns></returns>
        public static ICollection<Document> GetDocumentsByIds(ICollection<int> documentIds)
        {
            IList<Document> result = new List<Document>();
            foreach(int documentId in documentIds)
            {
                result.Add(new Document(documentId));
            }
            return result;
        }

        /// <summary>
        /// Will extract and return a collection of node ids sparated by comma - CSV format.
        /// Time: O(n), Space: O(n), Where n - number of ids.
        /// </summary>
        /// <param name="csvIds"></param>
        /// <returns></returns>
        public static ICollection<int> GetNodeIdsFromCsv(string csvIds)
        {
            IList<int> result = new List<int>();
            if(string.IsNullOrWhiteSpace(csvIds))
                return result;
            string[] ids = csvIds.Split(',');
            foreach(string id in ids)
            {
                result.Add(Convert.ToInt32(id));
            }
            return result;
        }

        /// <summary>
        /// Will build csv content from given collection of node ids.
        /// Time: O(n), Space: O(n), Where n - number of node ids.
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public static string BuildCsvContent(ICollection<int> nodeIds)
        {
            StringBuilder result = new StringBuilder();
            int position = 0;
            foreach(int nodeId in nodeIds)
            {
                position++;
                result.Append(nodeId);
                if(position < nodeIds.Count)
                    result.Append(",");
            }
            return result.ToString();
        }

        /// <summary>
        /// Returns the value (not null and empty) of the property with the given alias recursively
        /// Time: O(n), Space O(n); where n - content tree height.
        /// </summary>
        /// <param name="node">The node</param>
        /// <param name="propertyAlias">The alias of the property</param>
        /// <returns>The property's value</returns>
        public static string GetPropertyValueRecursive(INode node, string propertyAlias)
        {
            if(node.GetProperty(propertyAlias) != null && !string.IsNullOrEmpty(node.GetProperty(propertyAlias).Value))
                return node.GetProperty(propertyAlias).Value;

            if(node.Parent != null)
                return GetPropertyValueRecursive(node.Parent, propertyAlias);

            return string.Empty;
        }

        /// <summary>
        /// Saves and publishes a document
        /// Time: O(1), Space: O(1); where n - number of nodes on content tree.
        /// </summary>
        /// <param name="document">The document to save and publish</param>
        public static void SaveAndPublish(Document document)
        {
            if(document != null)
            {
                document.Save();
                document.Publish(document.User);
                umbraco.library.UpdateDocumentCache(document.Id);
            }
        }

        /// <summary>
        /// Will find all nodes of given contant type.
        /// Time: undetermined, Space: undetermined
        /// </summary>
        /// <param name="contantTypeAlias">Content type alias to match</param>
        /// <returns>List of INode of given content type.</returns>
        public static ICollection<INode> GetAllNodesByConetntType(string contantTypeAlias)
        {
            IList<INode> result = new List<INode>();
            INode startNode = new Node(-1); // start at root of content tree.
            return PreorderTraverse(contantTypeAlias, startNode, new List<INode>());
        }
        private static ICollection<INode> PreorderTraverse(string contantTypeAlias, INode startNode, ICollection<INode> result)
        {
            if(startNode.NodeTypeAlias == contantTypeAlias)
            {
                result.Add(startNode);
            }
            foreach(INode child in startNode.ChildrenAsList)
            {
                result = (PreorderTraverse(contantTypeAlias, child, result));
            }
            return result;
        }

        public static ICollection<INode> GetNodesFromMultiNodeTreePicker(string nodeIds)
        {
            IList<INode> result = new List<INode>();
            ICollection<int> Ids = GetNodeIdsFromMultiNodeTreePicker(nodeIds);
            foreach(int id in Ids)
            {
                result.Add(new Node(id));
            }
            return result;
        }

        public static INode GetFirstNodeOfType(string documentTypeAlias, int parentId)
        {
            INode parent = new Node(parentId);
            foreach(INode child in parent.ChildrenAsList)
            {
                if(child.NodeTypeAlias == documentTypeAlias)
                    return child;
            }
            return null;
        }
    }
}

/*
 * Creator: Stefan Kip
 * © InfoCaster 2010
 */
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.presentation.nodeFactory;
using umbraco.cms.businesslogic.web;

namespace InfoCaster.Umbraco.Library
{
	public static class UmbracoExtensions
	{
		public static string DefaultMLLanguage { get; set; }
		public static string MLDelimiter { get; set; }

		/// <summary>
		/// Returns the child nodes as IEnumerable collection
		/// </summary>
		/// <param name="node">The parent node</param>
		/// <returns>The child nodes as IEnumerable</returns>
		public static IEnumerable<Node> GetChildNodes(this Node node)
		{
			return node.Children.OfType<Node>();
		}

		/// <summary>
		/// Returns the value (not null and empty) of the property with the given alias recursively
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <returns>The property's value</returns>
		public static string GetPropertyValueRecursive(this Node node, string propertyAlias)
		{
			if (node.GetProperty(propertyAlias) != null && !string.IsNullOrEmpty(node.GetProperty(propertyAlias).Value))
				return node.GetProperty(propertyAlias).Value;

			if (node.Parent != null)
				return GetPropertyValueRecursive(node.Parent, propertyAlias);

			return string.Empty;
		}

		/// <summary>
		/// Returns the multilanguage value (not null and empty) of the property with the given alias recursively
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <param name="lang">The two-letter language code (e.g. 'en', 'nl' or 'de')</param>
		/// <returns>The property's value</returns>
		public static string GetPropertyValueRecursive(this Node node, string propertyAlias, string lang)
		{
			UpdatePropertyAlias(ref propertyAlias, lang);

			return node.GetPropertyValueRecursive(propertyAlias);
		}

		/// <summary>
		/// Returns the value of the property with the given alias or @default
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <param name="default">The alternative default value delegate</param>
		/// <returns>The property's value or @default if empty</returns>
		public static string GetPropertyValueOrDefault(this Node node, string propertyAlias, Func<string> @default)
		{
			if (node.GetProperty(propertyAlias) != null && !string.IsNullOrEmpty(node.GetProperty(propertyAlias).Value))
				return node.GetProperty(propertyAlias).Value;

			if (@default != null)
				return @default();

			return null;
		}

		/// <summary>
		/// Returns the multilanguage value of the property with the given alias or @default
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <param name="default">The alternative default value delegate</param>
		/// <param name="lang">The two-letter language code (e.g. 'en', 'nl' or 'de')</param>
		/// <returns>The property's value or @default if empty</returns>
		public static string GetPropertyValueOrDefault(this Node node, string propertyAlias, Func<string> @default, string lang)
		{
			UpdatePropertyAlias(ref propertyAlias, lang);

			return node.GetPropertyValueOrDefault(propertyAlias, @default);
		}

		/// <summary>
		/// Returns the value of the property with the given alias or @default
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <param name="default">The alternative default value</param>
		/// <returns>The property's value or @default if empty</returns>
		public static string GetPropertyValueOrDefault(this Node node, string propertyAlias, string @default)
		{
			if (node.GetProperty(propertyAlias) != null && !string.IsNullOrEmpty(node.GetProperty(propertyAlias).Value))
				return node.GetProperty(propertyAlias).Value;

			return @default;
		}

		/// <summary>
		/// Returns the multilanguage value of the property with the given alias or @default
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <param name="default">The alternative default value</param>
		/// <param name="lang">The two-letter language code (e.g. 'en', 'nl' or 'de')</param>
		/// <returns>The property's value or @default if empty</returns>
		public static string GetPropertyValueOrDefault(this Node node, string propertyAlias, string @default, string lang)
		{
			UpdatePropertyAlias(ref propertyAlias, lang);

			return node.GetPropertyValueOrDefault(propertyAlias, @default);
		}

		/// <summary>
		/// Returns the value of the property with the given alias
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <returns>The property's value</returns>
		public static string GetPropertyValue(this Node node, string propertyAlias)
		{
			if (node.GetProperty(propertyAlias) != null && !string.IsNullOrEmpty(node.GetProperty(propertyAlias).Value))
				return node.GetProperty(propertyAlias).Value;

			return string.Empty;
		}

		/// <summary>
		/// Returns the multilanguage value of the property with the given alias
		/// </summary>
		/// <param name="node">The node</param>
		/// <param name="propertyAlias">The alias of the property</param>
		/// <param name="lang">The two-letter language code (e.g. 'en', 'nl' or 'de')</param>
		/// <returns>The property's value</returns>
		public static string GetPropertyValue(this Node node, string propertyAlias, string lang)
		{
			UpdatePropertyAlias(ref propertyAlias, lang);

			return node.GetPropertyValue(propertyAlias);
		}

		/// <summary>
		/// Checks if a target node is in the path of the current node
		/// </summary>
		/// <param name="node">The current node</param>
		/// <param name="target">The target node</param>
		/// <returns>True if the target node exists in the current node's path</returns>
		public static bool ContainsNodeInPath(this Node node, Node target)
		{
			if (target == null)
				throw new ArgumentNullException("target");

			Node current = node;
			while (current != null)
			{
				if (target.Id == current.Id)
					return true;

				current = current.Parent;
			}
			return false;
		}

		

		/// <summary>
		/// Checks if the DefaultMLLanguage is set and updates the propertyAlias
		/// </summary>
		/// <returns>The updated propertyAlias</returns>
		/// <exception cref="InvalidOperationException">DefaultMLLanguage is not set</exception>
		static void UpdatePropertyAlias(ref string propertyAlias, string lang)
		{
			if (string.IsNullOrEmpty(DefaultMLLanguage))
				throw new InvalidOperationException("DefaultMLLanguage has to be set first");

			if (lang != DefaultMLLanguage)
				propertyAlias = string.Format("{0}{1}{2}", propertyAlias, !string.IsNullOrEmpty(MLDelimiter) ? MLDelimiter : "_", lang);
		}
	}
}
*/