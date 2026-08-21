# Passo 5 — Criar a interface e a navegação

**Tempo sugerido: 30 minutos**

1. Abra uma nova sessão no modo **Agent**, com contexto restrito à interface, aos contratos
   compartilhados e à especificação de inscrições.
2. Peça um plano com rota da página, arquivos envolvidos e estados visuais.
3. Confirme como a página receberá o identificador do treinamento.
4. Aprove o plano antes de editar.
5. Crie a página de inscritos usando os componentes e padrões existentes.
6. Adicione à lista de treinamentos uma ação de navegação para a nova página.
7. Compile a solução antes de iniciar as aplicações.
8. Execute API e Client em terminais separados.
9. Abra a lista de treinamentos e navegue até os inscritos de um item.
10. Valide lista vazia, cadastro válido e tentativa duplicada.
11. Confirme que os dados digitados permanecem visíveis após um erro.
12. Revise o diff pela área **Source Control** do VS Code e remova qualquer funcionalidade
    fora do escopo. Como alternativa, use `git diff` no terminal.

A página deve:

- identificar claramente o treinamento selecionado;
- listar os inscritos já cadastrados;
- coletar nome, sobrenome e e-mail;
- representar carregamento, sucesso, lista vazia e erro;
- preservar os dados preenchidos quando a API rejeitar a inscrição;
- atualizar a lista depois de um cadastro bem-sucedido.

Na lista existente de treinamentos, adicione uma ação que leve à página de inscritos do item
selecionado. Não transforme a tela em um CRUD de treinamentos nem adicione navegação que não
seja necessária à fatia.

> [!TIP]
> Para prompt completo, rotas sugeridas, comandos e roteiro de teste manual, consulte as
> [instruções completas](https://github.com/{{ repository }}/blob/main/.github/help/05-criar-interface-navegacao.md).

Quando o fluxo estiver acessível pela lista de treinamentos, comente `integrado`.
