using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using edit.pure.diagram.core;
using edit.pure.diagram.serializer;
using edit.pure.inspector;
using plugins.diagram.canvas.mission.node;

namespace plugins.diagram.canvas.mission {
    internal class MissionExport_XML : IDiagramExporter {
        public string rootPath { get; set; }
        public string output { get; set; }
        public string diagramType { get; set; }
        private Diagram _canvas;
        private Dictionary<long, GraphNode_Chapter> _chapters = new Dictionary<long, GraphNode_Chapter>();
        private Dictionary<long, GraphNode_Mission> _missions = new Dictionary<long, GraphNode_Mission>();
        private Dictionary<long, GraphNode_MissionNodeBase> _nodes = new Dictionary<long, GraphNode_MissionNodeBase>();

        private XElement _fileRoot;


        public void Export() {
            _canvas = new MissionDiagram();
            _fileRoot = new XElement("root");
            _fileRoot.SetAttributeValue("type", "diagram");
            _fileRoot.SetAttributeValue("collection", "mission");
            DoFile(new DirectoryInfo(rootPath));

            Doexporter();
            new XDocument(_fileRoot).Save(output + "/" + diagramType + ".xml");
        }

        private void DoFile(FileSystemInfo info) {
            if (info.Exists == false) return;
            if (info is DirectoryInfo) {
                FileSystemInfo[] files = (info as DirectoryInfo).GetFileSystemInfos();
                for (int i = 0, len = files.Length; i < len; i++) {
                    DoFile(files[i]);
                }
            } else if (info is FileInfo) {
                if (info.Extension == ".diagram") {
                    Encode(info as FileInfo);
                }
            }
        }

        private void Encode(FileInfo info) {
            XDocument doc = XDocument.Load(info.FullName);
            if (doc.Root == null) return;
            string ct = Attribute(doc.Root, "type");
            if (diagramType == ct) {
                new XMLToDiagram(_canvas).Execute(doc.Root);
                foreach (var n in _canvas.nodes) {
                    if (n is GraphNode_Chapter) {
                        _chapters.Add(n.uuid, n as GraphNode_Chapter);
                    } else if (n is GraphNode_Mission) {
                        _missions.Add(n.uuid, n as GraphNode_Mission);
                    } else if (n is GraphNode_MissionNodeBase) {
                        _nodes.Add(n.uuid, n as GraphNode_MissionNodeBase);
                    }
                }
            }
        }

        private void Doexporter() {
            XElement c = new XElement("item");
            c.SetAttributeValue("type", "table");
            c.SetAttributeValue("name", "chapter");
            foreach (var u in _chapters.Values) {
                WriteChapte(u, c);
            }
            _fileRoot.Add(c);

            c = new XElement("item");
            c.SetAttributeValue("type", "table");
            c.SetAttributeValue("name", "mission");
            foreach (var u in _missions.Values) {
                WriteMission(u, c);
            }
            _fileRoot.Add(c);

            c = new XElement("item");
            c.SetAttributeValue("type", "table");
            c.SetAttributeValue("name", "node");
            foreach (var u in _nodes.Values) {
                WriteNode(u, c);
            }
            _fileRoot.Add(c);
        }

        private void WriteChapte(GraphNode_Chapter data, XElement parent) {
            XElement ele = new XElement("item");
            ele.SetAttributeValue("type", "chapter");
            ele.SetAttributeValue("id", data.uuid);
            PInspectorCore[] ps = data.GetProperties();
            foreach (var p in ps) {
                ele.SetAttributeValue(p.propertyName, p.GetValueString());
            }
            parent.Add(ele);
        }

        private void WriteMission(GraphNode_Mission data, XElement parent) {
            XElement ele = new XElement("item");
            ele.SetAttributeValue("type", "mission");
            ele.SetAttributeValue("id", data.uuid);
            PInspectorCore[] ps = data.GetProperties();
            foreach (var p in ps) {
                ele.SetAttributeValue(p.propertyName, p.GetValueString());
            }
            List<string> pids = new List<string>();
            foreach (var i in data.inputs) {
                if (i.connectTypeId == GraphType.CHAPTER) {
                    foreach (var o in i.connections) {
                        ele.SetAttributeValue("chapterId", o.body.uuid);
                    }
                } else if (i.connectTypeId == GraphType.MISSIOIN) {
                    foreach (var o in i.connections) {
                        pids.Add(o.body.uuid.ToString());
                    }
                }
            }
            ele.SetAttributeValue("input", string.Join(",", pids.ToArray()));

            pids.Clear();

            foreach (var o in data.outputs) {
                foreach (var i in o.connections) {
                    if (i.connectTypeId == GraphType.MISSIOIN) {
                        pids.Add(i.body.uuid.ToString());
                    }
                }
            }

            ele.SetAttributeValue("output", string.Join(",", pids.ToArray()));

            parent.Add(ele);
        }

        private void WriteNode(GraphNode_MissionNodeBase data, XElement parent) {
            XElement ele = new XElement("item");
            ele.SetAttributeValue("type", "node");
            ele.SetAttributeValue("id", data.uuid);
            ele.SetAttributeValue("nodeType", data.nodeType);
            PInspectorCore[] ps = data.GetProperties();
            foreach (var p in ps) {
                ele.SetAttributeValue(p.propertyName, p.GetValueString());
            }
            foreach (var i in data.inputs) {
                if (i.connectTypeId == GraphType.NODE) {
                    foreach (var o in i.connections) {
                        ele.SetAttributeValue("parent", o.body.uuid);
                        break;
                    }
                }
            }
            parent.Add(ele);
        }

        private string Attribute(XElement xml, string attributeName) {
            XAttribute att = xml.Attribute(attributeName);
            return att == null ? string.Empty : att.Value;
        }
    }
}