Gilded Rose Kata
=========================

Gilded Rose kata solution in csharp. A complete example to develop a library with NET Core, test it and deploy it as a package using differents cis.

WIP!

## Continuous Integration

| Name      | Operating System | Status |
| :---      | :---             | :---   |
| AppVeyor  | Windows          | [![AppVeyor build status](https://ci.appveyor.com/api/projects/status/aknwu9sil3dv3im0?svg=true)](https://ci.appveyor.com/project/jrgcubano/gildedrose) |
| Travis CI | Linux & Mac      | [![Travis build status](https://img.shields.io/travis/jrgcubano/GildedRose.svg?label=travis)](https://travis-ci.org/jrgcubano/GildedRose) |

## NuGet Packages

| Name | .NET Version | NuGet |
| :--- | :--- | :---  |
| [GildedRose](https://www.nuget.org/packages/GildedRose/) | NET Core | [![GildedRose NuGet Package](https://img.shields.io/nuget/v/GildedRose.svg)](https://www.nuget.org/packages/GildedRose/)

## Used Stuff

* Platform
   * [NET Core](https://github.com/dotnet/core)
* Code Editor
   * [Visual studio code](https://code.visualstudio.com/) (.vscode directory)
* Code style
   * [Editor config](http://editorconfig.org/)   
* Tests
   * [xUnit](https://github.com/xunit/xunit)
   * [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
* Tests Coverage
   * [OpenCover](https://github.com/OpenCover/opencover)
* Code analyzer
   * [StyleCop Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
* Build Automation
   * [Cake](https://github.com/cake-build/cake)
* CI/CD
   * [AppVeyor](https://www.appveyor.com/) (Windows) 
   * [Travis](travis-ci.org) (Linux, OSX)
* Deployed as package to
   * [NuGet](https://www.nuget.org/)
   * [MyGet](http://www.myget.org/)
* Git and GitHub tools
   * [Commitizen](https://github.com/commitizen/cz-cli) (git commit style)
   * [Hub](https://github.com/github/hub) (github pull-requests, ci status, releases, etc)

## Kata Instructions
1. Clone your selected language starter from [Starters](https://github.com/emilybache/GildedRose-Refactoring-Kata). To clone just the subfolder with your language of choice: 
```
git init gilded-rose-java (start a new repo)
cd gilded-rose-java
git remote add origin https://github.com/emilybache/GildedRose-Refactoring-Kata.git
git config core.sparsecheckout true
echo "Java/*" >> .git/info/sparse-checkout (filter by subfolder Java, csharp, etc; and download only that one)
git pull --depth=1 origin master (download the subfolder language starter)
```
2. Specifications:

Hi and welcome to team Gilded Rose. As you know, we are a small inn with a prime location in a
prominent city ran by a friendly innkeeper named Allison. We also buy and sell only the finest goods.
Unfortunately, our goods are constantly degrading in quality as they approach their sell by date. We
have a system in place that updates our inventory for us. It was developed by a no-nonsense type named
Leeroy, who has moved on to new adventures. Your task is to add the new feature to our system so that
we can begin selling a new category of items. First an introduction to our system:

	- All items have a SellIn value which denotes the number of days we have to sell the item
	- All items have a Quality value which denotes how valuable the item is
	- At the end of each day our system lowers both values for every item

Pretty simple, right? Well this is where it gets interesting:

	- Once the sell by date has passed, Quality degrades twice as fast
	- The Quality of an item is never negative
	- "Aged Brie" actually increases in Quality the older it gets
	- The Quality of an item is never more than 50
	- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	- "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
	Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
	Quality drops to 0 after the concert

We have recently signed a supplier of conjured items. This requires an update to our system:

	- "Conjured" items degrade in Quality twice as fast as normal items

Feel free to make any changes to the UpdateQuality method and add any new code as long as everything
still works correctly. However, do not alter the Item class or Items property as those belong to the
goblin in the corner who will insta-rage and one-shot you as he doesn't believe in shared code
ownership (you can make the UpdateQuality method and Items property static if you like, we'll cover
for you).

Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a
legendary item and as such its Quality is 80 and it never alters.
