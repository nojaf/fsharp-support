namespace JetBrains.ReSharper.Plugins.FSharp.Tests.Features

open JetBrains.ReSharper.FeaturesTestFramework.Intentions
open JetBrains.ReSharper.Plugins.FSharp.Psi.Features.Daemon.QuickFixes
open JetBrains.ReSharper.Plugins.FSharp.Tests.Common
open NUnit.Framework

[<FSharpTest>]
type ReplaceWithWildPatTest() =
    inherit QuickFixTestBase<ReplaceWithWildPatFix>()

    override x.RelativeTestDataPath = "features/quickFixes/replaceWithWildPat"

    [<Test>] member x.``Let - Bang 01``() = x.DoNamedTest()
    [<Test>] member x.``Let - Value 01``() = x.DoNamedTest()

    [<Test>] member x.``Param - Function 01``() = x.DoNamedTest()
    [<Test>] member x.``Param - Function 02``() = x.DoNamedTest()
    [<Test>] member x.``Param - Method 01``() = x.DoNamedTest()

    [<Test>] member x.``Match clause pat 01``() = x.DoNamedTest()

    [<Test; NotAvailable>] member x.``Not available - Ctor 01``() = x.DoNamedTest()
    [<Test; NotAvailable>] member x.``Not available - Function 01``() = x.DoNamedTest()
