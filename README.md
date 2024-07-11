# Resolução do Desafio back-end PicPay Simplificado

![PicPay_Logogrande](https://github.com/Pauloocm/desafio-backend-picpay/assets/80168785/73bfb026-2c97-4720-9818-cac0f79c8016)

## Desafio

O PicPay Simplificado é uma plataforma de pagamentos simplificada. Nela é possível depositar e realizar transferências de dinheiro entre usuários. Temos 2 tipos de usuários, os comuns e lojistas, ambos têm carteira com dinheiro e realizam transferências entre eles.

Para ambos tipos de usuário, precisamos do Nome Completo, CPF, e-mail e Senha. CPF/CNPJ e e-mails devem ser únicos no sistema. Sendo assim, seu sistema deve permitir apenas um cadastro com o mesmo CPF ou endereço de e-mail;

Usuários podem enviar dinheiro (efetuar transferência) para lojistas e...


Leia o enunciado completo [clicando aqui](https://github.com/PicPay/picpay-desafio-backend?tab=readme-ov-file).


---


## Técnologias Utilizadas

![Dotnet](https://img.shields.io/badge/Dotnet-.NET-512BD4?logo=dotnet)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework%20Core-6.0-512BD4?logo=ef)
![NUnit](https://img.shields.io/badge/NUnit-3.13.3-22D3EE?logo=nunit)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-13-4169E1?logo=postgresql)


- **.NET 8 Web API**: Para criar uma interface robusta e escalável para o backend.
- **Entity Framework Core**: Para o mapeamento objeto-relacional (ORM).
- **NUnit**: Para testes unitários, garantindo a qualidade e a confiabilidade do código.
- **Unit of Work**: Para gerenciar transações e manter a consistência dos dados.
- **PostgreSQL**: Como banco de dados relacional.
- **Domain Driven Development (DDD)**: Para estruturar e organizar a aplicação, facilitando a manutenção e evolução do sistema.

## Arquitetura da Solução

![Desafio back-end picpay (1)](https://github.com/Pauloocm/desafio-backend-picpay/assets/80168785/7aae86ff-b68b-46b4-93dd-f25cb163e841)
