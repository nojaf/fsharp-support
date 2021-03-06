namespace JetBrains.ReSharper.Plugins.FSharp.Daemon.Stages

open JetBrains.ReSharper.Feature.Services.Daemon
open JetBrains.ReSharper.Plugins.FSharp.Daemon.Cs.Stages
open JetBrains.ReSharper.Plugins.FSharp.Psi.Tree

[<DaemonStage(StagesBefore = [| typeof<DeadCodeHighlightStage> |],
              StagesAfter = [| typeof<HighlightIdentifiersStage> |])>]
type SyntaxErrorsStage() =
    inherit FSharpDaemonStageBase()

    override x.CreateStageProcess(fsFile, _, daemonProcess) =
        SyntaxErrorsStageProcess(fsFile, daemonProcess) :> _


and SyntaxErrorsStageProcess(fsFile: IFSharpFile, daemonProcess) =
    inherit FcsErrorsStageProcessBase(fsFile, daemonProcess)

    override x.Execute(committer) =
        match fsFile.CheckerService.ParseFile(daemonProcess.SourceFile) with
        | Some parseResults -> x.Execute(parseResults.Errors, committer)
        | _ -> ()
