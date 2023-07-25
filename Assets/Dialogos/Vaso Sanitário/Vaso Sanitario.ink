INCLUDE ../globals.ink
EXTERNAL TocaFMODEvent(eventReference)
EXTERNAL ChangeInkVariable(variableName, newValue)
EXTERNAL TrocaScene(sceneToLoad)

{VasoSanitarioEntupido == "Não": -> main}
{VasoSanitarioEntupido == "Sim": -> vaso_entupiu}

=== main ===
    ~ TocaFMODEvent("event:/Interagiveis/VasoSanitario/VasoSanitarioDescarga")
    FLUUUSHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH... GLURB GLURB! #speaker: Vaso Sanitário
    (Parece haver algo preso na tubulação)
    * [Tentar novamente]
        ~ TocaFMODEvent("event:/Interagiveis/VasoSanitario/VasoSanitarioEntupido")
        ~ ChangeInkVariable("VasoSanitarioEntupido", "Sim")
        A descarga entupiu!
        ** [Olhar dentro do vaso]
            Ao observar o interior do vaso sanitário, você sente-se ligeiramente hipnotizado...
            ...
            ~ TrocaScene("Gincana do Barão Dantene")
            -> END
        ** [Desistir]
            -> END
    * [Desistir]
        -> END

=== vaso_entupiu ===
    ~ TocaFMODEvent("event:/Interagiveis/VasoSanitario/VasoSanitarioEntupido")
    A descarga está entupida! #speaker: Vaso Sanitário
    O que fazer?
    * [Olhar dentro do vaso]
        Ao observar o interior do vaso sanitário, você sente-se ligeiramente hipnotizado...
        ...
        ~ TrocaScene("Gincana do Barão Dantene")
        -> END
    * [Desistir]
        -> END