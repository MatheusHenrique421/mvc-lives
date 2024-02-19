# Projeto mvc-lives

Durante a pandemia do coronavírus, o time de desenvolvimento passou a desenvolver lives para alimentar o conhecimento da comunidade de tecnologia e arrecadar doações em dinheiro para ações de combate aos efeitos do Covid.

Essas lives serão ministradas por instrutores que falam de diversos assuntos.

Cada live poderá ter diversos inscritos.

O projeto visa desenvolver um breve sistema WEB (ASP.NET) que controle os cadastros e relacione as informações de maneira que o usuário possa gerenciar as lives, pessoas inscritas, instrutores e ainda gerar uma linha digitável de boleto para arrecadar doações.

Linguagem de programação .NET (Visual Basic ou C#) e Banco de dados SQL SERVER

Desenvolver o trabalho utilizando - Linguagem de programação .NET (Visual Basic ou C#) e - Banco de dados SQL SERVER As três partes abaixo contemplam o escopo mínimo do produto. 

Use sua criatividade para expandir a ideia.

## `Parte 1 - Tela principal`
- **A home será a primeira tela a ser apresentada na aplicação. Nela, o usuário terá as seguintes possibilidades:**
  * Acessar o cadastro de Instrutores 
  * Acessar o cadastro de Inscritos 
  * Acessar o cadastro de Lives 
  * Acessar o cadastro de Inscrições.

## `Parte 2 - Cadastros básicos, Realizar CRUD (Create, Read, Update e Delete)`
- **Cadastro de Instrutor. Um instrutor deve conter:**
  * Nome (String) 
  * Data de Nascimento (Data) 
  * Email (String)
  * Endereço no Instagram (String)
##
- **Cadastro de Inscritos. Um inscrito deve conter:**
  * Nome (String) 
  * Data de Nascimento (Data) 
  * Email (String)
  * Endereço no Instagram (String)
##
- **Cadastro de lives. Uma live deve conter:**
  * Nome (String) 
  * Descrição (String) 
  * Instrutor (Chave Estrangeira de Instrutor (integer)) 
  * Data e Hora de Início (Data) 
  * Duracao em Minutos (INTEIRO) 
  * Valor da Inscrição (DECIMAL)
##
- **Inscrições: Trata-se da entidade que ligará um inscrito a uma live, tendo os seguintes campos:** 
  * uma Live (Chave Estrangeira de Live (integer)) 
  * um Inscrito (Chave Estrangeira de Inscrito (integer)) 
  * Valor da Inscrição (Decimal) 
  * Data de vencimento (Data) 
  * Status de pagamento (tinyint: pago ou nao pago)

## `Parte 3 - Exibição da linha digitável na tela e liquidação do pagamento`
- **Ao editar uma inscrição, o usuário deverá ter a possibilidade de realizar duas ações:** 
  * marcar uma inscrição como PAGA.
  * Visualizar em tela o Número da Linha digitável do boleto a ser pago.
##
## `Vamos conhecer um pouco sobre a linha digitável de um boleto.`

Linha Digitável Os dados da linha digitável representam o conteúdo do código de barras, dispostos em outra ordem e acrescidos de dígitos verificadores nos três primeiros campos.

Deve ser utilizada quando há a impossibilidade da captura do código de barras e/ou para pagamentos em terminais de auto-atendimento, Internet, home/office bank, personal bank, etc.
##
Abaixo, a composição da linha digitável:

![This is an image](~/wwwroot/img/boleto1.png)

Referencia: https://boletocloud.uservoice.com/knowledgebase/articles/321057-o-boleto-partes-principais em 23/04/2020.

Quando o usuário acessar a inscrição e solicitar a linha digitavel da inscricao, o sistema deverá exibir o seguinte: 00000.00000 00000.000000 00000.000000 0 DDDDNNNNNNNNNN Nesse exercício faremos apenas a 5ª parte do código de barras, deixando as partes de 1 a 4 zeradas.

A 5ª Parte é composta pelo FATOR VENCIMENTO (4 digitos) e pelo VALOR DO BOLETO FORMATADO em 10 digitos.

Fator de vencimento O fator de vencimento é um referencial numérico de 4 dígitos, situado nas quatro primeiras posições do campo "valor", que representa a quantidade de dias decorridos da data base à data de vencimento do título.

A data base estipulada como o marco zero para o cálculo do fator de vencimento é 07.10.1997.

Para se obter o fator de vencimento se faz necessário apenas calcular o número de dias entre a data base e a data do vencimento.

A seguir alguns exemplos:

![This is an image](~/wwwroot/img/boleto2.png)
