using System.Collections.Generic;

namespace DataLoader.Maketalents.Models
{
    public class AuthResposeModel
    {
        public Account account { get; set; }
        public string authctoken { get; set; }
        public List<Role> roles { get; set; }
    }

    public class Partition
    {
        public string id { get; set; }
        public List<object> attributes { get; set; }
        public string name { get; set; }
    }

    public class Account
    {
        public string id { get; set; }
        public List<object> attributes { get; set; }
        public bool enabled { get; set; }
        public long createdDate { get; set; }
        public object expirationDate { get; set; }
        public Partition partition { get; set; }
        public string loginName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
    }

    public class Itplace
    {
        public bool view { get; set; }
        public bool globalEmail { get; set; }
    }

    public class Test
    {
        public bool view { get; set; }
        public bool edit { get; set; }
        public bool assign { get; set; }
    }

    public class EmailTemplate
    {
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class Administration
    {
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class Project
    {
        public bool view { get; set; }
        public bool edit { get; set; }
        public bool report { get; set; }
        public bool editProjectHistory { get; set; }
        public bool assign { get; set; }
    }

    public class Permission
    {
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class Training
    {
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class Video
    {
        public bool view { get; set; }
        public bool edit { get; set; }
        public bool assign { get; set; }
    }

    public class FreeEmployee
    {
        public bool viewEmployment { get; set; }
        public bool analyze { get; set; }
    }

    public class Employee
    {
        public bool view { get; set; }
        public bool edit { get; set; }
        public bool viewAttaches { get; set; }
        public bool editOwn { get; set; }
        public bool editStaff { get; set; }
        public bool delete { get; set; }
        public bool viewTimelines { get; set; }
        public bool viewStaff { get; set; }
    }

    public class WelcomeTraining
    {
        public bool view { get; set; }
    }

    public class CustomField
    {
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class Search
    {
        public bool view { get; set; }
    }

    public class Dictionary
    {
        public bool limitedView { get; set; }
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class VersionHistory
    {
        public bool edit { get; set; }
    }

    public class QualityScore
    {
        public bool view { get; set; }
        public bool edit { get; set; }
        public bool viewInGroup { get; set; }
        public bool editStaff { get; set; }
        public bool viewStaff { get; set; }
    }

    public class Report
    {
        public bool view { get; set; }
    }

    public class Course
    {
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class Action
    {
        public bool perspectiveEdit { get; set; }
        public bool reminderEdit { get; set; }
        public bool bonusView { get; set; }
        public bool viewInterviewWithMeWithoutFinance { get; set; }
        public bool taskView { get; set; }
        public bool vacancyPriceEdit { get; set; }
        public bool hiringEdit { get; set; }
        public bool surveyEdit { get; set; }
        public bool decreeView { get; set; }
        public bool surveyView { get; set; }
        public bool interviewEdit { get; set; }
        public bool importantfactEdit { get; set; }
        public bool bonusRemove { get; set; }
        public bool reminderView { get; set; }
        public bool hiringView { get; set; }
        public bool vacancyPriceView { get; set; }
        public bool firingEdit { get; set; }
        public bool bonusEdit { get; set; }
        public bool conversationViewEmployee { get; set; }
        public bool assignprojectEdit { get; set; }
        public bool groupchangeEdit { get; set; }
        public bool internshipView { get; set; }
        public bool achievementEdit { get; set; }
        public bool starratingEdit { get; set; }
        public bool testEdit { get; set; }
        public bool attestationEdit { get; set; }
        public bool importantfactView { get; set; }
        public bool trainingEdit { get; set; }
        public bool interviewView { get; set; }
        public bool taskEdit { get; set; }
        public bool assignvideoEdit { get; set; }
        public bool firingView { get; set; }
        public bool trainingView { get; set; }
        public bool conversationEditEmployee { get; set; }
        public bool decreeEdit { get; set; }
        public bool interviewViewNoFinanace { get; set; }
        public bool perspectiveView { get; set; }
        public bool testView { get; set; }
        public bool assignvideoView { get; set; }
        public bool attestationView { get; set; }
        public bool accessToOtherActions { get; set; }
        public bool internshipEdit { get; set; }
        public bool conversationEditApplicant { get; set; }
        public bool assignprojectView { get; set; }
        public bool groupchangeView { get; set; }
        public bool conversationViewApplicant { get; set; }
        public bool achievementView { get; set; }
        public bool starratingView { get; set; }
        public bool? conversationView { get; set; }
    }

    public class Vacation
    {
        public bool editFact { get; set; }
        public bool transferPlanned { get; set; }
        public bool view { get; set; }
        public bool editPlanned { get; set; }
        public bool editOwnPlanned { get; set; }
        public bool excelVacationExport { get; set; }
    }

    public class Event
    {
        public bool view { get; set; }
        public bool edit { get; set; }
    }

    public class Vacancy
    {
        public bool view { get; set; }
        public bool viewPublic { get; set; }
        public bool edit { get; set; }
    }

    public class Verbs
    {
        public Itplace itplace { get; set; }
        public Test test { get; set; }
        public EmailTemplate emailTemplate { get; set; }
        public Administration administration { get; set; }
        public Project project { get; set; }
        public Permission permission { get; set; }
        public Training training { get; set; }
        public Video video { get; set; }
        public FreeEmployee freeEmployee { get; set; }
        public Employee employee { get; set; }
        public WelcomeTraining welcomeTraining { get; set; }
        public CustomField customField { get; set; }
        public Search search { get; set; }
        public Dictionary dictionary { get; set; }
        public VersionHistory versionHistory { get; set; }
        public QualityScore qualityScore { get; set; }
        public Report report { get; set; }
        public Course course { get; set; }
        public Action action { get; set; }
        public Vacation vacation { get; set; }
        public Event @event { get; set; }
        public Vacancy vacancy { get; set; }
    }

    public class Role
    {
        public string name { get; set; }
        public object type { get; set; }
        public string id { get; set; }
        public Verbs verbs { get; set; }
    }

}
