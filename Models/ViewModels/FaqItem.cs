namespace DomcheBGLTD.Models.ViewModels;

public record FaqItem(string Question, string Answer)
{
    public static IEnumerable<FaqItem> Seed() => new[]
    {
        new FaqItem("How do I post a listing?",
            "Create an account, click “Post Listing” and fill the form."),
        new FaqItem("Is there a fee?",
            "No, posting and browsing are free for this university demo."),
        new FaqItem("Can I edit my ad after publishing?",
            "Yes — go to My Listings and click Edit.")
    };
}