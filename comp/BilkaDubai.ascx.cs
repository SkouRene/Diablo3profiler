using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Collections;
using System.Text;
using System.Security.Cryptography;


public partial class BilkaDubai : System.Web.UI.UserControl
{
    private string host;
    private string authToken;
    private string surveyObject;
    private string surveyId;
    private string name;
    private string lastName;
    private string mobil;
    private string email;
    private string terms;
    private string answer;
    private string falkNewsmail;
    private string bilkaNewsmail;
    private string surveyResonseUrl;
    private string strBilkaUserName = "BilkaContacts-12";
    private string strBilkaPassword = "20120817_ws_BilkaContacts";

    protected void Page_Load(object sender, EventArgs e)
    {    
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ValidateTerms();
    }
    protected void ValidateTerms()
    {
        if (cbTerms.Checked)
        {
            CreateSurveyResponse();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", "<script type=\"text/javascript\">alert('Advarsel du har ikke accepteret vilkårene')</script>");
        }
    }
    protected void CreateSurveyResponse()
    {
        string isFalk = (cbFalkNyhedsbrev.Checked)?"true":"false";
        string isBilka = (cbBilkaNyhedsbrev.Checked) ? "true" : "false";

        //setting token and host
        host = "https://restapi.surveygizmo.com/v2/survey/";
        surveyId = "1014096";
        surveyObject = "/surveyresponse?_method=PUT";
        answer = GetDubaiAnswer(rblstQuestion.SelectedValue);
        name = "&data[56]=" + txtbName.Text;
        lastName = "&data[57]=" + txtbLastname.Text;
        mobil = "&data[58]=" + txtbMobil.Text;
        email = "&data[59]=" + txtbEmail.Text;
        terms = "&data[60][10135]=1";
        falkNewsmail = "&data[60][10136]="+isFalk;
        bilkaNewsmail = "&data[60][10137]=" + isBilka;
        authToken = "&user:md5=tr@juhlsen.com:" + CreateMD5Hash("70201950");

        //create url
        surveyResonseUrl = host + surveyId + surveyObject + answer + name + lastName + mobil + email + terms + falkNewsmail + bilkaNewsmail + authToken;
        //send survey response
        dynamic surveyResponseResult = getObjectFromURL(surveyResonseUrl);

        if (surveyResponseResult.result_ok == true)
        {
            Response.Redirect("/thankyou.aspx");
             //?-fjg pray all went well here - look Thomas is all ok or ?

            using (ServiceReference1.ContactFunctionsSoapClient ws = new ServiceReference1.ContactFunctionsSoapClient())
            {
                try
                {
                    ws.SubscribeFromQuiz(strBilkaUserName, strBilkaPassword, txtbName.Text + txtbLastname.Text, txtbEmail.Text, "dubaiquiz");
                }
                catch (Exception exc)
                {
                    string b = exc.ToString();
                    //Helper.SendErrorMail("Jfejl i Dansk Supermarked dubaiquiz: kald til subscribe Bilka newsletter webservice.", exc);
                }
            }

        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", "<script type=\"text/javascript\">alert('hmm den gik ikke')</script>");
        }
        

    }
    protected string GetDubaiAnswer(string radioValue)
    {
        string value = null;

        switch (radioValue)
        {
            case "istanbul":
                value = "&data[55][10132]=1";
                
                break;
            case "dubai":
                value = "&data[55][10133]=1";
                
                break;
            case "rom":
                value = "&data[55][10134]=1";
                
                break;
        }
        return value;
    }
    public dynamic getObjectFromURL(string URL)
    {
        using (var webClient = new System.Net.WebClient())
        {
            var json = webClient.DownloadString(URL);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            dynamic jsonObject = jss.Deserialize(json, typeof(object)) as dynamic;
            return jsonObject;
        }
    }
    public string CreateMD5Hash(string input)
    {
        // Use input string to calculate MD5 hash
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
            // To force the hex string to lower-case letters instead of
            // upper-case, use he following line instead:
            // sb.Append(hashBytes[i].ToString("x2")); 
        }
        return sb.ToString();
    }

    
}
/// <summary>
/// Summary description for DynamicJsonConverter
/// </summary>
public class DynamicJsonConverter : JavaScriptConverter
{
    public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
    {
        if (dictionary == null)
            throw new ArgumentNullException("dictionary");

        if (type == typeof(object))
        {
            return new DynamicJsonObject(dictionary);
        }

        return null;
    }

    public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Type> SupportedTypes
    {
        get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
    }
}
/// <summary>
/// Summary description for DynamicJsonObject
/// </summary>
public class DynamicJsonObject : DynamicObject
{
    private IDictionary<string, object> Dictionary { get; set; }

    public DynamicJsonObject(IDictionary<string, object> dictionary)
    {
        this.Dictionary = dictionary;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        result = this.Dictionary[binder.Name];

        if (result is IDictionary<string, object>)
        {
            result = new DynamicJsonObject(result as IDictionary<string, object>);
        }
        else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
        {
            result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
        }
        else if (result is ArrayList)
        {
            result = new List<object>((result as ArrayList).ToArray());
        }

        return this.Dictionary.ContainsKey(binder.Name);
    }
}