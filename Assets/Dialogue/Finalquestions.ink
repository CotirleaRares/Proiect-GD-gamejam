VAR correct = false

-> start

== start ==
-> question1

== question1 ==
The player enters the café and approaches the counter. What do you say?
+ Bonjour, je voudrais un café, s'il vous plaît.
    ~ correct = true
    -> question2
+ Salut, donne-moi un café !
    ~ correct = false
    -> question2
+ J'aime le café.
    ~ correct = false
    -> question2
+ Merci, au revoir !
    ~ correct = false
    -> question2

== question2 ==
~ correct = false
The barista asks: "Vous avez choisi ?" (Have you decided?)
+ Oui, je voudrais un espresso, s'il vous plaît.
    ~ correct = true
    -> question3
+ Non, au revoir.
    ~ correct = false
    -> question3
+ Je suis un espresso.
    ~ correct = false
    -> question3
+ C'est un café.
    ~ correct = false
    -> question3

== question3 ==
~ correct = false
The barista asks: "Quel type de café voulez-vous ?"
+ Un café au lait, s'il vous plaît.
    ~ correct = true
    -> question4
+ Un croissant, merci.
    ~ correct = false
    -> question4
+ Un café et une pizza.
    ~ correct = false
    -> question4
+ Je veux une table.
    ~ correct = false
    -> question4

== question4 ==
~ correct = false
The barista asks: "Grande ou petit ?" (Big or small?)
+ Un petit café, s'il vous plaît.
    ~ correct = true
    -> question5
+ Oui, merci.
    ~ correct = false
    -> question5
+ Un café rouge, s'il vous plaît.
    ~ correct = false
    -> question5
+ Je suis grand.
    ~ correct = false
    -> question5

== question5 ==
 ~ correct = false
The barista asks: "Avec du sucre ?" (With sugar?)
+ Oui, avec un sucre, s'il vous plaît.
    ~ correct = true
    -> question6
+ Non, avec du lait.
    ~ correct = false
    -> question6
+ Un sucre, au revoir.
    ~ correct = false
    -> question6
+ Je suis sucré.
    ~ correct = false
    -> question6

== question6 ==
 ~ correct = false
The barista tells the price. What do you say?
+ Voilà cinq euros.
    ~ correct = true
    -> question7
+ Tu es cher.
    ~ correct = false
    -> question7
+ Je veux un autre café.
    ~ correct = false
    -> question7
+ Un billet, au revoir !
    ~ correct = false
    -> question7

== question7 ==
~ correct = false
You receive your coffee. How do you respond?
+ Merci beaucoup !
    ~ correct = true
    -> question8
+ Je veux un croissant !
    ~ correct = false
    -> question8
+ Bonjour !
    ~ correct = false
    -> question8
+ J'aime le café.
    ~ correct = false
    -> question8

== question8 ==
 ~ correct = false
You're about to leave. What do you say?
+ Merci, bonne journée !
    ~ correct = true
    -> end
+ Bonne nuit !
    ~ correct = false
    -> end
+ D'accord, merci.
    ~ correct = false
    -> end
+ Je vais au supermarché.
    ~ correct = false
    -> end

== end ==
-> DONE
