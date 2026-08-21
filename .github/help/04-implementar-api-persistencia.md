# Passo 4 — Implementar API e persistência - Instruções completas

Esta é a etapa mais longa do lab. Trabalhe em incrementos e valide cada decisão antes de
avançar. A especificação aprovada pela equipe é a autoridade.

## 1. Preparar uma conversa limpa

1. Abra uma nova conversa no modo **Agent**.
2. Adicione ao contexto:
   - `.github/copilot-instructions.md`;
   - `docs/specs/training-attendees-vertical-slice.md`;
   - `docs/specs/training-catalog-vertical-slice.md`;
   - `src/Application`;
   - `src/Api/Program.cs`;
   - `src/Infrastructure`;
   - `src/Tests/Api.Tests`.
3. Não inclua os arquivos do Client: a interface será tratada no passo seguinte.

Uma conversa nova reduz a chance de o Copilot continuar decisões exploratórias da etapa de
especificação como se já fossem aprovadas.

## 2. Solicitar o plano

Envie:

```text
Leia as instruções e as especificações relevantes. Inspecione os padrões existentes em
Application, Api, Infrastructure e Api.Tests.

Planeje, sem editar, a implementação da fatia aprovada de cadastro e listagem de inscritos.

O plano deve apresentar:
1. contratos públicos de requisição e resposta;
2. rotas e resultados HTTP;
3. entidade, relacionamento e chave estrangeira;
4. índice que garante unicidade por treinamento;
5. estratégia única de normalização do e-mail;
6. arquivos que serão criados ou alterados;
7. testes pela API pública;
8. sequência de implementação e comandos de validação.

Restrições:
- preserve todos os contratos existentes de treinamentos;
- use Entity Framework Core e SQLite já configurados;
- não crie cadastro global de aluno;
- não imponha unicidade global do e-mail;
- mantenha detalhes de persistência fora dos contratos públicos;
- não implemente edição, exclusão, autenticação, paginação ou interface;
- gere uma migration, mas não a aplique antes da revisão.

Pare depois do plano e aguarde minha aprovação.
```

## 3. Revisar o plano

Use esta tabela:

| Ponto | O que deve estar claro |
| --- | --- |
| rota | `trainingId` identifica o treinamento na URL |
| corpo | contém nome, sobrenome e e-mail, sem repetir `trainingId` |
| inexistência | cadastro e listagem retornam `404` |
| validação | campos inválidos retornam `400` com `errors` |
| duplicidade | mesmo e-mail no mesmo treinamento retorna `409` |
| normalização | aplicação e restrição do banco usam a mesma representação |
| relacionamento | inscrito depende de um treinamento existente |
| testes | usam a API pública e SQLite isolado |

Peça ajustes se o plano criar repositórios genéricos, serviços sem necessidade, cadastro de
alunos ou mais operações do que a especificação exige.

## 4. Aprovar as decisões explicitamente

Não responda apenas "pode implementar". Assim como na etapa de especificação, registre em um
prompt quais decisões a equipe realmente aprovou. Isso reduz o risco de o agente tratar uma
sugestão não discutida como autorização.

Adapte e envie:

```text
Nossa equipe revisou o plano e aprovou estas decisões de implementação:

- os contratos existentes de treinamentos serão preservados;
- os contratos HTTP e critérios da especificação de inscritos serão mantidos;
- o inscrito será dependente de um treinamento, sem cadastro global de aluno;
- a entidade de inscrito terá chave estrangeira para o treinamento;
- o e-mail será normalizado removendo espaços externos e desconsiderando diferenças entre
  letras maiúsculas e minúsculas;
- a unicidade será garantida pela combinação de treinamento e e-mail normalizado;
- a representação normalizada permanecerá interna e não será exposta no DTO público;
- o treinamento será verificado antes do cadastro e da listagem;
- validação, inexistência e duplicidade usarão os status e o formato de erros especificados;
- os testes usarão a API pública e um banco SQLite isolado;
- a migration será gerada somente depois dos testes direcionados;
- a migration será revisada antes de ser aplicada;
- interface, edição, exclusão, autenticação e paginação não serão implementadas neste passo.

Considere somente essas decisões como aprovadas. Ainda não edite arquivos.

Recapitule:
1. os incrementos na ordem em que serão executados;
2. os arquivos previstos em cada incremento;
3. a validação executada ao final de cada incremento;
4. o ponto exato em que deverá parar para nova revisão.

Se alguma decisão for incompatível com a especificação ou com o código existente, sinalize
agora. Caso contrário, pare depois da recapitulação e aguarde minha autorização.
```

Compare a recapitulação com a especificação e com a tabela da seção anterior. Se houver uma
divergência, corrija-a por meio de outro prompt. Quando tudo estiver correto, autorize apenas o
primeiro incremento:

```text
Decisões confirmadas. Pode iniciar somente o primeiro incremento aprovado: contratos
compartilhados e modelo de persistência. Pare novamente depois do build e da apresentação do
diff.
```

## 5. Implementar contratos e modelo

O agente já recebeu a autorização acima. Se precisar retomar a conversa, use:

```text
Implemente primeiro somente os contratos compartilhados e o modelo de persistência aprovados.
Configure relacionamento e índice composto no DbContext. Ainda não gere a migration.

Ao terminar:
1. mostre os arquivos alterados;
2. explique como o índice representa a regra;
3. execute o build;
4. pare para revisão.
```

Revise:

- o DTO público não expõe campos internos de normalização;
- a entidade possui a chave estrangeira necessária;
- o índice combina treinamento e e-mail normalizado;
- o e-mail não ficou único globalmente;
- exclusão em cascata, se configurada, foi uma decisão consciente.

## 6. Implementar os endpoints

Continue:

```text
Implemente agora os endpoints de cadastro e listagem conforme a especificação.

Reutilize o formato de erros existente. Garanta que:
- o treinamento seja verificado;
- a entrada seja validada antes de persistir;
- o e-mail seja normalizado de forma consistente;
- conflito de unicidade produza o contrato 409 aprovado;
- a resposta de criação informe a localização;
- a listagem pertença somente ao treinamento da rota.

Execute o build e pare antes dos testes. Abra **Source Control** no VS Code e selecione cada
arquivo alterado para revisar sua comparação. Como alternativa, apresente `git diff` no
terminal.
```

Confira o contrato esperado:

| Operação | Rota | Resultado |
| --- | --- | --- |
| cadastrar | `POST /api/trainings/{trainingId}/attendees` | `201`, `400`, `404` ou `409` |
| listar | `GET /api/trainings/{trainingId}/attendees` | `200` ou `404` |

## 7. Criar os testes

Peça:

```text
Adicione testes funcionais pela API pública, seguindo a factory e o isolamento SQLite
existentes. Cubra somente:
1. cadastro válido e localização;
2. listagem após cadastro;
3. nome, sobrenome e e-mail inválidos;
4. treinamento inexistente no POST e GET;
5. duplicidade com variações de caixa e espaços;
6. confirmação de que a duplicidade não armazenou outro item;
7. mesmo e-mail permitido em treinamentos diferentes.

Execute primeiro somente os novos testes. Se falharem, investigue antes de alterar o contrato.
```

Execute também manualmente, se necessário:

```bash
dotnet test src/Tests/Api.Tests/TrainingCatalog.Api.Tests.csproj
```

Leia as falhas. Não aceite uma correção que enfraqueça a especificação apenas para fazer o
teste passar.

## 8. Gerar e revisar a migration

Confirme antes se a ferramenta está disponível:

```bash
dotnet ef --version
```

O resultado deve indicar a versão da ferramenta Entity Framework Core. Se o terminal informar
que `dotnet-ef` não existe, que o comando não foi encontrado ou que nenhuma ferramenta
correspondente está instalada, instale a mesma versão principal e secundária usada pelos
pacotes do projeto:

```bash
dotnet tool install --global dotnet-ef --version 10.0.11
```

Depois, disponibilize as ferramentas globais no terminal atual e repita a verificação:

```bash
export PATH="$PATH:$HOME/.dotnet/tools"
dotnet ef --version
```

O último comando deve informar `Entity Framework Core .NET Command-line Tools 10.0.11`. Se a
instalação disser que a ferramenta já está instalada, não reinstale: execute somente o
`export PATH` e teste novamente. Se ainda não funcionar, abra um novo terminal do Codespace,
execute `dotnet ef --version` e prossiga apenas depois que a versão for exibida.

> [!NOTE]
> A versão `10.0.11` não foi escolhida arbitrariamente. Ela corresponde às referências
> `Microsoft.EntityFrameworkCore.Design` e `Microsoft.EntityFrameworkCore.Sqlite` existentes
> no projeto. Não instale outra versão sem antes verificar esses pacotes.

Gere a migration:

```bash
dotnet ef migrations add AddTrainingAttendees \
  --project src/Infrastructure \
  --startup-project src/Api
```

Não execute `database update` ainda. Peça:

```text
Use a skill `review-ef-migration` para revisar a migration recém-gerada.

Verifique:
- tabela e colunas;
- nulabilidade;
- chave primária;
- chave estrangeira;
- comportamento de exclusão;
- índice composto de unicidade;
- alterações inesperadas em tabelas existentes;
- coerência do snapshot.

Não aplique nem edite até apresentar os achados.
```

Abra os arquivos da migration e confirme os achados do Copilot.

## 9. Aplicar e validar

Depois de aprovar a revisão da migration, continue na mesma conversa com o agente e envie:

```text
A revisão da migration foi aprovada. Aplique-a ao banco de desenvolvimento e valide toda a
solução.

Execute, nesta ordem:

1. `dotnet ef database update --project src/Infrastructure --startup-project src/Api`;
2. `dotnet build src/TrainingCatalog.slnx`;
3. `dotnet test src/TrainingCatalog.slnx --no-build`.

Apresente o resultado de cada comando antes de concluir.

Se algum comando falhar:
1. pare a sequência no comando que falhou;
2. leia a mensagem de erro completa e identifique a causa provável;
3. relacione a causa à especificação, à migration ou ao código alterado;
4. proponha a menor correção necessária;
5. não altere contratos aprovados nem enfraqueça testes para obter sucesso;
6. mostre os arquivos que pretende alterar e aguarde minha aprovação;
7. depois da aprovação, aplique a correção;
8. reexecute primeiro o comando que falhou;
9. quando ele passar, reexecute build e toda a suíte de testes.

Não gere outra migration, remova o banco nem reverta a migration aprovada sem explicar a
necessidade e solicitar autorização.

Ao final, informe:
- se a migration foi aplicada;
- quantos testes passaram;
- quais arquivos foram alterados para corrigir eventuais falhas;
- qualquer divergência restante.
```

Opcionalmente, inspecione a estrutura:

```bash
sqlite3 src/Api/training-catalog.db ".schema"
```

Procure a tabela de inscritos, a chave estrangeira e o índice composto.

## 10. Verificação final

- [ ] contratos públicos correspondem à especificação;
- [ ] todos os endpoints antigos continuam compilando e passando nos testes;
- [ ] cadastro e listagem possuem testes pela API pública;
- [ ] duplicidade por treinamento está protegida na aplicação e no banco;
- [ ] o mesmo e-mail é permitido em treinamentos diferentes;
- [ ] migration e snapshot foram revisados;
- [ ] nenhuma funcionalidade de interface foi implementada.

Volte à issue e comente `persistido`.
