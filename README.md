# Teste FullStack - INOVAGE

Projeto para a vaga de desenvolvedor SAP pleno da empresa INOVAGE.

## 🛠 Tecnologias Usadas

As seguintes tecnologias foram usadas para desenvolver esse projeto:
```
 - C#
 - Bootstrap
 - Angular
 - TypeScript
 - ASP.NET CORE
```

### 📋 Pré-requisitos

```
 - Node.js
 - Npm
 - .NET 8.0 Runtime
```

## 🚀 Detalhes

### ⚙️ BackEnd:
O backend foi desenvolvido utilizando ASP.NET Core para criar uma API RESTfull que realiza operações CRUD (Create, Read, Update, Delete) integradas à Service Layer da equipe Inovage de Partners. Essa abordagem foi escolhida para melhorar a segurança e simplificar a manipulação de dados, além de facilitar a adaptação às possíveis mudanças na API externa.

Os endpoints implementados permitem:

**GET** /businesspartners: Recupera todos os parceiros de negócios disponíveis.
**POST** /businesspartners: Cria um novo parceiro de negócios.
**PUT** /businesspartners/{businessPartnerId}: Atualiza o nome de um parceiro de negócios existente.
**DELETE** /businesspartners/{businessPartnerId}: Remove um parceiro de negócios pelo ID.

Essa API também foi configurada com:

**Swagger UI:** Para documentação e testes mais práticos dos endpoints.
**CORS:** Permite o acesso de origens específicas, configurado através de um arquivo de configuração dedicado. Foi implementado pois o frondend não estava conseguindo acessar o endpoint por conta da politica do "servidor".
**HTTPS:** Garantindo a segurança na comunicação com o cliente.


### 🖥️ FrontEnd:
**Exibição de Parceiros de Negócios:**
Criei uma tabela dinâmica que lista os parceiros, exibindo informações como CardCode, CardName, City e Country. Os dados são carregados automaticamente a partir da API backend.

**CRUD Funcional:**
Criar: Possibilidade de adicionar novos parceiros diretamente na interface.
Editar: Permite a atualização do nome do parceiro na tabela de forma inline.
Excluir: Remoção de parceiros de maneira rápida e prática.
Feedback Visual: Exibe uma mensagem de carregamento enquanto os dados do backend estão sendo inicializados. Adicionei botões com ícones e tooltips para ações, como criar, editar e excluir, garantindo uma experiência de usuário mais clara.
Responsividade: Fiz a utilização do grid system do Bootstrap para garantir uma boa exibição em diferentes tamanhos de tela.

## ✒️ Autor
* **Caio Portugal** - *Desenvolvimento Completo* - [perfil](https://www.linkedin.com/in/caiosantosportugal/)
