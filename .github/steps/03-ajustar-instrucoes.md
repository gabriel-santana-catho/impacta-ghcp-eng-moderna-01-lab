# Passo 3 — Ajustar o contexto durável

**Tempo sugerido: 10 minutos**

Leia `.github/copilot-instructions.md`. A instrução atual foi escrita quando só existia a
primeira fatia vertical e manda o Copilot consultar sempre a especificação do catálogo.
Agora há mais de uma especificação, então essa regra pode fornecer contexto incompleto ou
indevido.

1. Abra `.github/copilot-instructions.md` e identifique a referência fixa à primeira
   especificação.
2. Liste os documentos atuais em `docs/specs/`.
3. Pergunte ao Copilot por que a referência fixa pode causar contexto incorreto.
4. Peça uma alteração mínima, sem editar ainda.
5. Compare o trecho atual e o proposto.
6. Aprove somente uma regra que:

   - preserve propósito, plataforma e validações existentes;
   - exija a leitura das especificações relevantes para a solicitação atual;
   - não trate uma especificação antiga como autoridade para todo comportamento futuro;
   - exija contrato explícito para novos comportamentos e sinalização de conflitos;
   - não copie os detalhes da nova especificação para o arquivo de instruções.

7. Aplique a mudança e revise o diff pela área **Source Control** do VS Code. Como alternativa,
   use `git diff -- .github/copilot-instructions.md` no terminal.
8. Faça uma pergunta sobre inscritos ao Copilot e confirme que ele encontra a especificação
   nova sem esquecer os contratos existentes.

Antes de aceitar, abra **Source Control** no VS Code, selecione o arquivo alterado e examine as
linhas removidas e adicionadas. Como alternativa, use
`git diff -- .github/copilot-instructions.md`. O arquivo deve orientar **como trabalhar** no
repositório; `docs/specs/` deve registrar **o que o produto deve fazer**.

> [!TIP]
> Para um prompt pronto e critérios de revisão, consulte as
> [instruções completas](https://github.com/{{ repository }}/blob/main/.github/help/03-ajustar-instrucoes.md).

Quando a instrução estiver atualizada e revisada, comente `contextualizado`.
