# Passo 6 — Validar a fatia completa - Instruções completas

O objetivo final é demonstrar que cada critério aprovado possui uma evidência reproduzível.
Uma tela aparentemente correta, isoladamente, não comprova persistência nem regra de negócio.

## 1. Preparar a matriz

Abra `docs/specs/training-attendees-vertical-slice.md`. Crie temporariamente uma lista com três
colunas:

| Critério | Evidência | Resultado |
| --- | --- | --- |
| copie o critério | teste, resposta, migration ou navegador | pendente/aprovado/falhou |

Percorra todos os critérios. Não marque como aprovado algo que você não executou ou
inspecionou.

## 2. Executar as validações automatizadas

Na raiz do repositório:

```bash
dotnet restore src/TrainingCatalog.slnx
dotnet build src/TrainingCatalog.slnx --no-restore
dotnet test src/TrainingCatalog.slnx --no-build
```

Registre quantos testes passaram. Se houver falha:

1. identifique o primeiro teste relevante;
2. compare expectativa e especificação;
3. determine se a falha é no teste, implementação ou ambiente;
4. corrija somente a causa comprovada;
5. execute primeiro o teste direcionado;
6. repita a suíte completa.

## 3. Revisar a persistência

Abra a migration de inscritos e o snapshot. Confirme:

- tabela e colunas esperadas;
- campos obrigatórios não anuláveis;
- chave estrangeira para treinamentos;
- índice único composto por treinamento e e-mail normalizado;
- ausência de unicidade global do e-mail;
- ausência de alterações inesperadas na tabela de treinamentos.

Se desejar comprovar no banco:

```bash
sqlite3 src/Api/training-catalog.db ".schema"
```

## 4. Repetir a validação no navegador

Inicie API e Client como no passo anterior e execute:

1. abrir inscritos pela lista de treinamentos;
2. observar lista vazia ou itens existentes;
3. cadastrar um inscrito válido;
4. confirmar atualização da lista;
5. repetir o e-mail com variação de caixa ou espaços;
6. confirmar erro e preservação do formulário;
7. abrir outro treinamento;
8. confirmar separação das listas.

Associe cada observação a um critério.

## 5. Solicitar uma revisão sem edição

Selecione o custom agent de revisão criado no walkthrough ou use o modo **Ask**:

```text
Leia as especificações relevantes e revise o diff atual sem editar.

Relacione cada critério de aceitação da fatia de inscritos a uma evidência concreta em teste,
contrato, migration ou interface. Execute somente os comandos documentados de build e testes.

Procure especialmente:
- contrato implementado diferente da especificação;
- unicidade global em vez de unicidade por treinamento;
- comparação de e-mail sensível a caixa ou espaços;
- ausência de 404 para treinamento inexistente;
- duplicidade que ainda armazena outro item;
- UI que perde dados em erro;
- inscritos de um treinamento exibidos em outro;
- regressão nos endpoints existentes.

Informe primeiro falhas verificáveis e depois lacunas de evidência. Não sugira funcionalidades
fora do escopo e não edite arquivos.
```

Compare a revisão com sua matriz. Uma afirmação do agente não substitui a evidência.

## 6. Revisar o conjunto de mudanças

```bash
git status --short
git diff --check
git diff --stat
```

Abra **Source Control** na barra lateral do VS Code. Em **Changes**, selecione cada arquivo
para abrir a comparação lado a lado e percorra todas as linhas adicionadas e removidas.
Alternativamente, use `git diff` no terminal para ver o conteúdo completo. Procure:

- arquivos temporários ou bancos criados acidentalmente;
- alterações fora da fatia;
- contratos duplicados;
- comentários ou nomes incoerentes;
- código que não é alcançado;
- especificação diferente do comportamento final.

Se o comportamento aprovado mudou durante a implementação, atualize a especificação
explicitamente e repita as evidências afetadas. Não ajuste o documento apenas para justificar
um bug.

## 7. Matriz mínima de evidências

| Critério | Evidência mínima |
| --- | --- |
| entrada válida | resposta `201` e teste automatizado |
| campos inválidos | resposta `400` e teste |
| treinamento inexistente | resposta `404` no POST e GET e testes |
| duplicidade no treinamento | resposta `409`, teste e índice composto |
| duplicidade não armazenada | listagem ou contagem no teste |
| mesmo e-mail em outro treinamento | teste automatizado |
| persistência e listagem | consulta após cadastro |
| acesso pela lista | navegação executada no navegador |
| sucesso da interface | confirmação e lista atualizada |
| erro da interface | mensagem e formulário preservado |
| ausência de regressão | suíte completa aprovada |

## 8. Verificação final

- [ ] todos os critérios possuem evidência;
- [ ] build e suíte completa passam;
- [ ] migration e índice foram inspecionados;
- [ ] caminho feliz e duplicidade foram repetidos no navegador;
- [ ] diff não contém arquivos ou funcionalidades fora do escopo;
- [ ] divergências conhecidas foram registradas;
- [ ] a equipe consegue explicar as decisões sem depender do Copilot.

Volte à issue e comente `validado`.
