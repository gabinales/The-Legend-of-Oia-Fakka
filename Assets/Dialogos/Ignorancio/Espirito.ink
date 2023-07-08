INCLUDE ../globals.ink

//INCLUDE IgnorancioVars.ink

VAR quest_active = 0

{quest_active == 0: ->Dialogo1 | ->Dialogo2}

=== Dialogo1 ===
oOooOoOoo...
Ooõoòo0oºOo.o0ó...
->Choices

=== Choices ===
 +[Ignorar]
    ok #speaker: Ignorancio
  ->END
  
 +[Quem está aí?]
    Quem está aí? #speaker: Svard
    ~SPAWN = 1
    Eu, o Ignorancio #speaker: Ignorancio
    quest active?? : {quest_active}
    
 ->END
 
 === Dialogo2 ===
 e entao, como vai a busca?
 ->END


