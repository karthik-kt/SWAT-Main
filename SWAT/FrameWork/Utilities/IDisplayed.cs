
namespace SWAT.FrameWork.Utilities
{
    using OpenQA.Selenium;
    using System.Collections;
    interface IDisplay
    {
        bool IsDiplayed();
    }
    interface IEnable
    {
        bool IsEnabled();
    }
    interface IEdit 
    {
        bool Edit();
        bool ContainsValue();
    }
    interface ISelect 
    {
        bool Select(string value);
        bool ContainsValues(ICollection values,bool matchAll = true);
        bool SelectedValue(string value);
    }
    public class BasicWebElement: IEnable, IDisplay
    {
        public BasicWebElement(IWebElement webElement)
        {

        }
        public bool IsDiplayed()
        {
            return true;
        }
        public bool IsEnabled()
        {
            return true;
        }
        public virtual void Click()
        {
            return;
        }
    }
    internal class TextBox : BasicWebElement, IEdit
    {
        IWebElement _WebElement;
        string _Value;
        public TextBox(IWebElement webElement, string value = null) : base(webElement)
        {
            _WebElement = webElement;
            _Value = value;
        }

        public bool Edit()
        {
            return true;
        }

        public bool ContainsValue()
        {
            return true;
        }
    }
}
