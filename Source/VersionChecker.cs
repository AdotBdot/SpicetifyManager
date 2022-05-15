using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace SpicetifyManager.Source
{
    internal static class VersionChecker
    {
        public static async Task<string> GetLastTag(string owner, string repoName)
        {
            GitHubClient git = new(new ProductHeaderValue("Tag"));
            Repository repo = await git.Repository.Get(owner, repoName);
            IReadOnlyList<RepositoryTag> tags = await git.Repository.GetAllTags(repo.Id);

            return tags[0].Name;
        }
    }
}