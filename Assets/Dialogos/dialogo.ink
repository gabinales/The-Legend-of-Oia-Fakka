INCLUDE globals.ink

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