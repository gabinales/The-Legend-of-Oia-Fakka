INCLUDE globals.ink

->main
=== main ===
{Galho_Comum == 0: -> sem_galhos}
{Galho_Comum == 1: -> um_galho}
{Galho_Comum == 2: -> dois_galhos}
{Galho_Comum == 3: -> tres_galhos}
{Galho_Comum > 3: -> muitos_galhos}



=== sem_galhos ===
    NÃO TEM GALHOS!!!!
    +[Adicionar galho]
    ~Galho_Comum++
    ->main

=== um_galho ===
    TEM UM GALHO ! #speaker: Mr. Orange
    -> END

=== dois_galhos ===
    TEM DOIS GALHOS !! #speaker: Mr. Orange
    -> END
    
=== tres_galhos ===
    TEM TRÊS GALHOS !!! #speaker: Mr. Orange
    -> END
    
=== muitos_galhos ===
    AGORA ENFIA NO CU !!!! #speaker: Mr. Cu
    -> END
    

















/*INCLUDE globals.ink

Ola Svard #speaker: Camponês

{pokemon_name == "": ->Choices | ->ja_escoheu}

=== Choices ===
o que tu diz?
 +[Charmander]
  ->chosen("Charmander")

 +[Bulbasaur]
 ->chosen("Bulbasaur")

 +[Squirtle]
     ->chosen("Squirtle")

=== chosen(pokemon) ===
~pokemon_name = pokemon
Vc escolheu o {pokemon}. Nem fale mais comigo. Meu nome é Abu abdul al-Rahman #speaker: Abu abdul al-Rahman
ok #speaker: Svard
->END

=== ja_escoheu ===
vc ja escolheu o {pokemon_name}
->END
*/