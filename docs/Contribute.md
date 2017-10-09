## Introduction

You can contribute to codeRR by opening a Pull Request.

If your are new to git or github, the following articles may help you:

* See "Using Pull Requests":https://help.github.com/articles/using-pull-requests for more information about Pull Requests.

* See <a href="http://help.github.com/forking/">Fork A Repo</a> for an introduction to forking a repository and creating branches.

## Setting up user information

Please have git setup with consistent user information before sending a patch. Preferably with your real name and a working email address.

For quick reference, here are the relevant git commands:

<pre>
git config --global user.name "Your Name Comes Here"
git config --global user.email you@yourdomain.example.com
</pre>

## Fixing a bug

* In most cases, pull requests for bug fixes should be based on the @dev@ branch. There are exceptions, for example corrections to bugs that have been introduced in the @master@ branch.

* Write tests *before* fixing the bug (so that you will know that the test case catches the bug). 

## Adding a new feature

* In most cases, pull requests for new features should be based on the @dev@ branch.

* It is important to write a good commit message explaining **why** the feature is needed. We prefer that the information is in the commit message, so that anyone that want to know two years later why a particular feature can easily find out. It does no harm to provide the same information in the pull request (if the pull request consists of a single commit, the commit message will be added to the pull request automatically).

* With few exceptions, it is mandatory to write unit tests that tests the feature. The unit tests is required to ensure that the features does not stop working in the future.

* If you are implementing a new feature, also update the [documentation](https://github.com/coderrapp/coderr.documentation) to describe the feature (submit it using a new pull request that refers to this one).

* Make sure the patch does not break backward compatibility. In general, we only break backward compatibility in major releases and only for a very good reason and usually after first deprecating the feature one or two releases beforehand.

## Before you submit your pull request

* Make sure existing tests don't fail.

* Make sure that your branch contains clean commits:
** Commit messages that actually are understandable and related to the change.
** Make separate commits for separate changes. If you cannot describe what the commit does in one sentence, it is probably a mix of changes and should be separated into several commits.
** Don't merge @dev@ or @master@ into your branch. Use @git rebase@ if you need to resolve merge conflicts or include the latest changes.
** To make it possible to use the powerful @git bisect@ command, make sure that each commit can be compiled and that it works.
** Check for unnecessary whitespace before committing with @git diff --check@.

* Check your coding style:
** Make sure your changes follow the coding and indentation style of the code surrounding your changes.
** Do not commit commented-out code or files that are no longer needed. Remove the code or the files.
** We use Resharpers built in coding styles (which are based upon [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions))

Have fun!

*Help can be found in our [forum](http://discuss.coderrapp.com)*