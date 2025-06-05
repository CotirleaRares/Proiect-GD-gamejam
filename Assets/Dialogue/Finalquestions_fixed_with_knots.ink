VAR correct = false

-> start

== start ==
-> question1

== question1 ==
The player enters the café and approaches the counter. What do you say?
~ correct = false
+ Bonjour, je voudrais un café, s'il vous plaît.
    ~ correct = true
    -> question2
+ Salut, donne-moi un café !
    -> question2
+ J'aime le café.
    -> question2
+ Merci, au revoir !
    -> question2

== question2 ==
The barista asks: "Vous avez choisi ?"
~ correct = false
+ Oui, je voudrais un espresso, s'il vous plaît.
    ~ correct = true
    -> question3
+ Non, au revoir.
    -> question3
+ Je suis un espresso.
    -> question3
+ C'est un café.
    -> question3

== question3 ==
The barista asks: "Quel type de café voulez-vous ?"
~ correct = false
+ Un café au lait, s'il vous plaît.
    ~ correct = true
    -> question4
+ Un croissant, merci.
    -> question4
+ Un café et une pizza.
    -> question4
+ Je veux une table.
    -> question4

== question4 ==
The barista asks: "Grande ou petit ?"
~ correct = false
+ Un petit café, s'il vous plaît.
    ~ correct = true
    -> question5
+ Oui, merci.
    -> question5
+ Un café rouge, s'il vous plaît.
    -> question5
+ Je suis grand.
    -> question5

== question5 ==
The barista asks: "Avec du sucre ?"
~ correct = false
+ Oui, avec un sucre, s'il vous plaît.
    ~ correct = true
    -> question6
+ Non, avec du lait.
    -> question6
+ Un sucre, au revoir.
    -> question6
+ Je suis sucré.
    -> question6

== question6 ==
The barista tells the price. What do you say?
~ correct = false
+ Voilà cinq euros.
    ~ correct = true
    -> question7
+ Tu es cher.
    -> question7
+ Je veux un autre café.
    -> question7
+ Un billet, au revoir !
    -> question7

== question7 ==
You receive your coffee. How do you respond?
~ correct = false
+ Merci beaucoup !
    ~ correct = true
    -> question8
+ Je veux un croissant !
    -> question8
+ Bonjour !
    -> question8
+ J'aime le café.
    -> question8

== question8 ==
You're about to leave. What do you say?
~ correct = false
+ Merci, bonne journée !
    ~ correct = true
    -> end
+ Bonne nuit !
    -> end
+ D'accord, merci.
    -> end
+ Je vais au supermarché.
    -> end

== end ==
-> DONE
