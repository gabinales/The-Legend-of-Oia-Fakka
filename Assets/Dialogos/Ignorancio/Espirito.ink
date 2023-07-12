INCLUDE ../globals.ink
EXTERNAL StartQuest(id)
EXTERNAL FinishQuest(id)

{HoraDoDia == 2: ->Noite | -> NaoNoite}

=== Noite ===
//Quest em andamento
{questActive == 1: e entao, como vai a busca? ->END}
//Quest terminada
{questActive == 2: ->Terminou} 
//PrimeiroDialogo
{questActive == 0: ->PrimeiroDialogo}

=== NaoNoite ===
//Quest em andamento
{questActive == 1: Svard... Tão Claro... ->END}
//Quest Terminada, porem de dia
{questActive == 2: Svard... Tão Claro... ->END}
//PrimeiroDialogo
{questActive == 0: ->PrimeiroDialogo}

=== PrimeiroDialogo ===
oOooOoOoo...
Ooõoòo0oºOo.o0ó...

+[Ignorar]
    ok #speaker: Svard 
  ->END
  
 +[Quem está aí?]
    Tem alguém aí? 
    {HoraDoDia != 2: tão claro... | ->SeRevela} 
     ->END
 
 
 === SeRevela ===
 você pode me ouvir? #speaker: ???
 ~SPAWN = 1
 ~questActive = 1
 ~StartQuest("RufarDosTambores")
 Me chamo Ignorâncio e agora você está em uma quest. Ache meu Vinil. #speaker: Ignorancio
 ->END
 
 === Terminou ===
BOA 
 ~FinishQuest("RufarDosTambores")
TERMINOU
->END
 

