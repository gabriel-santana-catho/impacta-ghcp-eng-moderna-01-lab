# Plano do lab — Inscritos em treinamentos

## Objetivo pedagógico

Permitir que os alunos reutilizem, de forma autônoma, o ciclo praticado no Módulo 01:

**necessidade resumida → especificação → contexto durável → API → persistência → interface → evidências**

O lab não introduz turmas nem cadastro global de alunos. O modelo de negócio é
intencionalmente simples: um treinamento possui muitos inscritos, e um e-mail pode aparecer
apenas uma vez em cada treinamento.

## Duração e progressão

| Passo | Tempo | Entrega | Palavra |
| --- | ---: | --- | --- |
| 1. Preparar o ambiente | 10 min | linha de base executada e arquitetura localizada | `preparado` |
| 2. Especificar inscrições | 20 min | nova especificação revisada em `docs/specs/` | `especificado` |
| 3. Ajustar instructions | 10 min | seleção contextual de especificações | `contextualizado` |
| 4. API e persistência | 40 min | cadastro, listagem, migration, unicidade e testes | `persistido` |
| 5. Interface e navegação | 30 min | página de inscritos acessível pela lista | `integrado` |
| 6. Validar a entrega | 10 min | critérios relacionados a evidências | `validado` |
| **Total** | **120 min** | | |

Cada passo apresenta somente objetivo, limites e critérios suficientes para a tentativa
autônoma. No final há um link para `.github/help/`, onde ficam prompts, comandos, decisões
sugeridas e exemplos completos. O aluno escolhe quando recorrer à ajuda.

## Escopo funcional sugerido

- `POST /api/trainings/{trainingId}/attendees`;
- `GET /api/trainings/{trainingId}/attendees`;
- nome, sobrenome e e-mail obrigatórios;
- validação básica do formato do e-mail;
- comparação de e-mail normalizada;
- unicidade composta por treinamento e e-mail normalizado;
- `404` para treinamento inexistente;
- `409` para duplicidade no mesmo treinamento;
- cadastro e listagem na interface;
- acesso à página de inscritos por uma ação em cada item da lista de treinamentos.

Edição, exclusão, paginação, autenticação, turmas e cadastro separado de alunos permanecem
fora do escopo.

## Estratégia de orientação

Os comentários de transição dos workflows não apenas anunciam o próximo passo. Eles explicam
o valor do que acabou de ser concluído:

1. a linha de base separa regressão de falha preexistente;
2. a especificação converte intenção em contrato verificável;
3. as instructions selecionam contexto sem duplicar regras de produto;
4. API, índice e testes criam defesas complementares;
5. a interface fecha a jornada do usuário;
6. a matriz de evidências confirma a entrega supervisionada.

Essa devolutiva é importante porque o instrutor não acompanhará cada grupo continuamente.

## Organização do repositório publicado

O repositório final deve possuir somente:

- `main`: solução de referência concluída, além dos arquivos do lab;
- `inicio`: projeto final do walkthrough original e os arquivos do lab, sem a implementação
  de inscritos.

Não são necessários checkpoints intermediários.

### Montagem recomendada

1. Preserve o estado atual, que reutiliza `src` da branch `main` de
   `01-desenvolvimento-assistido`, como branch `inicio`.
2. Execute o lab a partir de `inicio` e ajuste especificação, prompts e tempo com base na
   execução real.
3. Leve o resultado aprovado para `main`.
4. Confirme que `main` e `inicio` contêm `.github/steps`, `.github/help`,
   `.github/scripts/progress-step.sh` e os workflows do lab.
5. Marque o repositório como template e mantenha a opção **Include all branches** destacada no
   README.
6. Antes da publicação, faça uma cópia de teste do template e percorra toda a issue.

O workflow inicial bloqueia a atividade quando a branch `inicio` não foi copiada. Como no
walkthrough original, os workflows das etapas futuras devem estar desabilitados no
repositório-template; ao iniciar, o workflow habilita somente a próxima etapa.

## Critérios para a execução piloto

- A cópia do template cria uma única issue do lab.
- A primeira etapa aparece somente quando `inicio` existe.
- Uma palavra incorreta não avança a atividade.
- A palavra correta publica primeiro a devolutiva e depois o passo seguinte.
- Todos os links de ajuda apontam para a cópia do aluno, não para o template original.
- O aluno consegue concluir o caminho principal em até 120 minutos.
- A ajuda completa permite recuperar um grupo bloqueado sem entregar código-fonte pronto.
- A solução de referência em `main` satisfaz os mesmos critérios, ainda que sua organização
  interna seja diferente da solução dos alunos.
