# Passo 5 — Criar a interface e a navegação - Instruções completas

Agora a API já possui contrato e persistência validados. Este passo cria a jornada do usuário
sem alterar novamente o comportamento do servidor.

## 1. Preparar o contexto da interface

1. Abra uma nova conversa no modo **Agent**.
2. Adicione:
   - `.github/copilot-instructions.md`;
   - a especificação de inscritos;
   - os contratos em `src/Application`;
   - `src/Client/Pages/Index.razor`;
   - `src/Client/Pages/Index.razor.css`;
   - arquivos de layout e configuração do Client.
3. Não adicione migrations ou testes de infraestrutura ao contexto inicial.

## 2. Pedir um plano visual e técnico

```text
Leia a especificação de inscritos, os contratos compartilhados e os padrões atuais do Client.
Planeje, sem editar, a interface da nova fatia.

O plano deve mostrar:
1. rota da página de inscritos e parâmetro recebido;
2. como identificar o treinamento selecionado;
3. como carregar a lista;
4. campos e validações do formulário;
5. estados de carregamento, vazio, sucesso e erro;
6. comportamento após cadastro válido;
7. comportamento após duplicidade;
8. alteração mínima na lista de treinamentos para navegar até a página;
9. arquivos envolvidos e validações.

Restrições:
- reutilize MudBlazor e os padrões existentes;
- reutilize os contratos compartilhados;
- não altere API ou persistência;
- não implemente edição ou exclusão;
- não misture o formulário de inscritos ao cadastro de treinamento.

Pare depois do plano.
```

## 3. Revisar o plano

Confirme que:

- a rota pode ser aberta para um treinamento específico;
- o título ou outra identificação útil do treinamento aparece na página;
- a lista vazia não é tratada como erro;
- o botão de envio é protegido durante a requisição;
- erros de campo podem ser associados aos inputs;
- `409` produz uma mensagem útil;
- o formulário não é limpo em caso de falha;
- a lista é atualizada depois do sucesso;
- existe um caminho claro para voltar ao catálogo.

## 4. Implementar a página

Autorize:

```text
Implemente primeiro a página de inscritos com a rota
`/trainings/{trainingId:guid}/attendees`.

Inclua carregamento do treinamento e dos inscritos, lista vazia, formulário, estados de envio,
mensagens de sucesso e erro. Preserve os dados em falha.

Não altere ainda a página inicial. Execute o build e pare para revisão. Abra **Source Control**
no VS Code e selecione a nova página para examinar a comparação. Como alternativa, use
`git diff -- caminho/da/nova/pagina`.
```

Revise nomes, textos, contratos serializados e tratamento de respostas. Se o Copilot duplicar
um contrato já disponível em `Application`, peça para reutilizá-lo.

## 5. Vincular a lista de treinamentos

Depois:

```text
Adicione à lista existente de treinamentos uma ação clara para abrir a página de inscritos do
item selecionado. Preserve o formulário e o comportamento atual da página inicial.

Inclua somente a navegação necessária e execute o build.
```

Na área **Source Control** do VS Code, selecione `Index.razor` e examine as linhas adicionadas
e removidas. Como alternativa, use `git diff -- src/Client/Pages/Index.razor`. Confirme que
cada item envia seu próprio identificador e que o restante do catálogo não foi reestruturado
sem necessidade.

## 6. Executar API e Client

Abra dois terminais.

No primeiro:

```bash
dotnet run --project src/Api --launch-profile http --urls http://127.0.0.1:5221
```

No segundo:

```bash
dotnet run --project src/Client --launch-profile http --urls http://127.0.0.1:5152
```

Aguarde as mensagens de inicialização. O Codespace pode perguntar se deseja abrir ou tornar
uma porta pública; para este teste, basta abrir a porta encaminhada no navegador.

## 7. Executar o roteiro manual

1. Abra o Client.
2. Cadastre um treinamento ou use um já existente.
3. Na lista, clique na ação de inscritos.
4. Confirme que a página identifica o treinamento correto.
5. Confirme o estado vazio.
6. Tente enviar o formulário sem preencher os campos.
7. Preencha nome, sobrenome e um e-mail válido.
8. Envie e observe o estado de carregamento.
9. Confirme a mensagem de sucesso e o item na lista.
10. Tente novamente com o mesmo e-mail, mudando caixa ou adicionando espaços.
11. Confirme a mensagem de duplicidade.
12. Confirme que os dados digitados não foram apagados.
13. Volte à lista de treinamentos e abra outro item.
14. Confirme que os inscritos do primeiro treinamento não aparecem no segundo.

Se algum resultado divergir, descreva o comportamento ao Copilot e referencie o critério da
especificação. Evite pedir "corrija tudo"; forneça a evidência concreta.

## 8. Validar e revisar

Pare as aplicações com <kbd>Ctrl</kbd>+<kbd>C</kbd> e execute:

```bash
dotnet build src/TrainingCatalog.slnx
dotnet test src/TrainingCatalog.slnx --no-build
git diff --check
```

Revise o diff pela área **Source Control** do VS Code: selecione cada arquivo para abrir a
comparação e percorra todas as alterações. Como alternativa, execute `git diff` no terminal.
Confirme:

- [ ] há uma página dedicada de inscritos;
- [ ] ela é acessível pela lista de treinamentos;
- [ ] estados de carregamento, vazio, sucesso e erro são visíveis;
- [ ] a lista atualiza após o sucesso;
- [ ] o formulário permanece preenchido após erro;
- [ ] não houve alteração de API, migration ou regra de negócio;
- [ ] edição e exclusão não foram adicionadas.

Volte à issue e comente `integrado`.
