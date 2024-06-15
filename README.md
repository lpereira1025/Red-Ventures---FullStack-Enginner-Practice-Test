<h1>Projeto de Pedidos de Ramen Online</h1>
Gostaria de agradecer pela oportunidade de participar do desafio prático para a posição de Fullstack Engineer. Abaixo, apresento minha abordagem e resposta para o case proposto.

<h3>Back-end</h3>
Para a construção da API do RamenGo, utilizei C# e ASP.NET Core, escolhendo essas tecnologias devido à minha experiência prévia e à robustez oferecida pelo framework ASP.NET Core para o desenvolvimento de APIs RESTful.<br>
<br>
<ul>Endpoints Criados:</ul>
<li>GET /caldos: Retorna as opções disponíveis de caldos para o ramen.</li>
<li>GET /proteinas: Retorna as opções disponíveis de proteínas para o ramen.</li>
<li>POST /orders: Recebe as seleções do usuário (caldos e proteínas) e processa o pedido, retornando um código de pedido.</li><br>
<br>
<ul>Endpoint para Geração de ID do Pedido:</ul>

<li>POST https://api.tech.redventures.com.br/orders/generate-id</li>

<h3>Front-end</h3>
No desenvolvimento do front-end, utilizei HTML, CSS e JavaScript puros, conforme solicitado, sem o uso de frameworks ou bibliotecas adicionais como React, Angular ou Bootstrap. Optei por utilizar Webpack como ferramenta de bundling e Sass como pré-processador CSS para melhor organização e eficiência no código.<br>
<br>
<ul>Funcionalidades Implementadas:</ul>

<li>Consumo dos endpoints /caldos e /proteinas para exibir as opções ao usuário.</li>
<li>Seleção de uma opção de cada lista de caldos e proteínas.</li>
<li>Requisição ao endpoint /orders com as seleções do usuário para criar um novo pedido.</li>
<li>Atualização da interface com base na resposta da API para informar ao usuário que seu pedido foi criado com sucesso.</li>
<li>Layout e Responsividade</li>
<li>Segui o layout e estilo especificados para desktop e mobile, garantindo uma experiência de usuário consistente em ambas as plataformas.</li><br>

<br>

<ul>Como Rodar o Backend Localmente:</ul>
<li>Certifique-se de ter o SDK do .NET Core instalado.</li>
<li>Clone este repositório.</li>
<li>Navegue até o diretório do backend.</li>
<li>Execute os comandos abaixo no terminal:</li>
<li>dotnet run</li>
<li>O backend estará disponível em http://localhost:5010.</li><br>
<br>
<ul>Como Rodar o Frontend Localmente:</ul>
<li>Abra o arquivo index.html no seu navegador web ou com a extensão Live Server</li>
