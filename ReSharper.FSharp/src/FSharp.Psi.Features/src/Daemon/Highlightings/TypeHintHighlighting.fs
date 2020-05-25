﻿namespace JetBrains.ReSharper.Plugins.FSharp.Psi.Features.Daemon.Highlightings

open System
open JetBrains.DocumentModel
open JetBrains.ProjectModel
open JetBrains.ReSharper.Feature.Services.Daemon.Attributes
open JetBrains.ReSharper.Feature.Services.Daemon
open JetBrains.TextControl.DocumentMarkup
open JetBrains.UI.RichText

[<DaemonIntraTextAdornmentProvider(typeof<TypeHintAdornmentProvider>)>]
[<StaticSeverityHighlighting(Severity.INFO,
     typeof<HighlightingGroupIds.IntraTextAdornments>,
     AttributeId = AnalysisHighlightingAttributeIds.PARAMETER_NAME_HINT,
     OverlapResolve = OverlapResolveKind.NONE,
     ShowToolTipInStatusBar = false)>]
type TypeHintHighlighting(typeNameString: string, range: DocumentRange) =
    let text = RichText(": " + typeNameString)

    interface IHighlighting with
        member x.ToolTip = null
        member x.ErrorStripeToolTip = null
        member x.IsValid() = not text.IsEmpty && not range.IsEmpty
        member x.CalculateRange() = range

    interface IHighlightingWithTestOutput with
        member x.TestOutput = text.Text

    member x.Text = text

and [<SolutionComponent>] TypeHintAdornmentProvider() =
    interface IHighlighterIntraTextAdornmentProvider with
        member x.CreateDataModel(highlighter) =
            match highlighter.UserData with
            | :? TypeHintHighlighting as thh ->
                { new IIntraTextAdornmentDataModel with
                    override x.Text = thh.Text
                    override x.HasContextMenu = false
                    override x.ContextMenuTitle = null
                    override x.ContextMenuItems = null
                    override x.IsNavigable = false
                    override x.ExecuteNavigation _ = ()
                    override x.SelectionRange = Nullable<_>()
                    override x.IconId = null
                    override x.IsPreceding = false
                    override x.Order = 0
                }
            | _ -> null