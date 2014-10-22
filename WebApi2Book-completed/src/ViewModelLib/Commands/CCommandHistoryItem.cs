using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModelLib.Commands
{
    public class CCommandHistoryItem
    {
        public string CommandName
        { get; set; }
        public UIElement ElementActedOn
        { get; set; }
        public string PropertyActedOn
        { get; set; }
        public object PreviousState
        { get; set; }
        public CCommandHistoryItem(string commandName)
            : this(commandName, null, "", null)
        { }
        public CCommandHistoryItem(string commandName, UIElement elementActedOn,
        string propertyActedOn, object previousState)
        {
            CommandName = commandName;
            ElementActedOn = elementActedOn;
            PropertyActedOn = propertyActedOn;
            PreviousState = previousState;
        }
        public bool CanUndo
        {
            get { return (ElementActedOn != null && PropertyActedOn != ""); }
        }
        public void Undo()
        {
            Type elementType = ElementActedOn.GetType();
            PropertyInfo property = elementType.GetProperty(PropertyActedOn);
            property.SetValue(ElementActedOn, PreviousState, null);
        }
    }
}
