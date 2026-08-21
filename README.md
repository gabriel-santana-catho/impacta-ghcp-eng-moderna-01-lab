# Lab — Inscritos em treinamentos

Neste lab do Módulo 01, você ampliará uma aplicação .NET 10 existente com uma nova fatia
vertical: cadastrar e listar inscritos (`attendees`) de cada treinamento.

Você partirá de uma necessidade resumida, produzirá uma especificação, ajustará o contexto
durável do GitHub Copilot e implementará API, persistência e interface sob supervisão humana.
O objetivo não é gerar código idêntico ao de uma solução de referência, mas comprovar os
contratos e comportamentos definidos pela sua equipe.

## Como iniciar

1. Use o botão abaixo para criar seu próprio repositório.
2. Na tela de criação, marque **Include all branches** para copiar a branch `inicio`.
3. Aguarde alguns segundos, atualize a página e abra a issue indicada no novo README.

[![Iniciar lab](https://img.shields.io/badge/Iniciar%20lab-%E2%86%92-1f883d?style=for-the-badge&logo=github&labelColor=197935)](https://github.com/new?template_owner=impacta-ghcp-eng-moderna&template_name=01-lab&owner=%40me&name=impacta-ghcp-eng-moderna-01-lab&description=M%C3%B3dulo+1%3A+lab+de+inscritos+em+treinamentos)

> [!IMPORTANT]
> Marque **Include all branches** antes de criar o repositório. O lab não usa checkpoints,
> mas depende da branch `inicio`, que contém o ponto de partida da atividade.

## Ponto de partida

A branch `inicio` reutiliza o resultado final do walkthrough
[`01-desenvolvimento-assistido`](https://github.com/impacta-ghcp-eng-moderna/01-desenvolvimento-assistido):

- API mínima em .NET 10;
- persistência com Entity Framework Core e SQLite;
- testes funcionais;
- interface Blazor WebAssembly;
- instruções, prompt file, skill e custom agent criados no Módulo 01.

O lab foi dimensionado para aproximadamente 120 minutos e será conduzido por uma issue.
