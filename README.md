# Caixa de Banco

## Índice
- [Visão Geral](#visao-geral)
- [Configuração](#configuracao)
- [Ferramentas Utilizadas](#ferramentas-utilizadas)
- [Documentação da API](#documentacao-da-api)
- [Licença](#licenca)

## Visão Geral
Caixa de Banco é uma aplicação bancária que permite aos usuários gerenciar suas contas e transações. Este projeto é construído utilizando .NET 8 e C# 12, e utiliza SQL Server hospedado na AWS para armazenamento de dados.

## Configuração

### Pré-requisitos
- Visual Studio ou Visual Code
- SDK do .NET 8

### Passos para Rodar Localmente
1. **Clonar o repositório**
    ```bash
    git clone https://github.com/nanaribeiro/caixa-de-banco.git
    ```
2. **Copiar pasta enviada por e-mail**
    - Extraia a pasta zipada enviada por email para o caminho C:\Users\SEUUSUARIO\AppData\Roaming\Microsoft. Substituindo SEUUSUARIO pelo usuario logado na maquina
2. **Abrir o projeto**
    - Abra o projeto no Visual Studio ou Visual Code.
3. **Rodar o projeto**
    - Pressione `F5` para compilar e rodar o projeto.
4. **Acessar a Documentação da API**
    - Abra o navegador de sua preferência e navegue para: [https://localhost:7009/swagger/index.html](https://localhost:7009/swagger/index.html)

*Nota: O banco de dados está hospedado na nuvem, então não são necessárias configurações adicionais. Você pode precisar restaurar os pacotes NuGet.*

## Ferramentas Utilizadas
- **.NET 8**
- **C# 12**
- **SQL Server (AWS)**
- **EF Core 9**
- **xUnit**

## Documentação da API
A documentação da API pode ser encontrada [aqui](https://github.com/nanaribeiro/caixa-de-banco/wiki/Documenta%C3%A7%C3%A3o-da-API).

## Licença
Este projeto é licenciado sob a Licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
