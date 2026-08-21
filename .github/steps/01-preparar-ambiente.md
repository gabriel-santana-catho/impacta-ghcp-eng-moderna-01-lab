# Passo 1 — Preparar o ambiente

**Tempo sugerido: 10 minutos**

Você trabalhará sobre a aplicação concluída no walkthrough do Módulo 01. Antes de pedir
qualquer alteração ao Copilot, confirme que a equipe está no ponto de partida correto e que
consegue executar as validações existentes.

[![Abrir no GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/{{ repository }}?quickstart=1)

1. Clique no botão acima para abrir a criação do Codespace.
2. Confirme que este repositório está selecionado e crie o ambiente.
3. Aguarde a preparação terminar. O SQLite será instalado automaticamente.
4. **Antes de qualquer outra atividade**, abra um terminal e execute `git switch inicio`.
5. Confirme que o .NET 10 e o SQLite estão disponíveis.
6. Restaure dependências, compile a solução e execute os testes existentes.
7. Se algo falhar, registre o comando e a mensagem antes de tentar corrigir.
8. Explore a solução e localize API, persistência, interface, testes, especificação e
   `.github/copilot-instructions.md`.
9. Abra a aplicação apenas se precisar entender o comportamento inicial.
10. Combine com a equipe quais arquivos pertencem a cada camada.

> [!IMPORTANT]
> O lab deve ser realizado na branch `inicio`, e não na `main`. Confirme a troca com
> `git branch --show-current`: o resultado precisa ser `inicio`. Se você continuar na `main`,
> partirá do estado errado e os passos seguintes poderão produzir resultados diferentes.

Não comece a implementar `attendees`. Ao final, todos devem saber onde procurar contratos,
endpoints, persistência, interface e testes, além de confirmar que a linha de base funciona.

> [!TIP]
> Se a equipe precisar de comandos e resultados esperados, consulte as
> [instruções completas](https://github.com/{{ repository }}/blob/main/.github/help/01-preparar-ambiente.md).

Quando todos estiverem no mesmo ponto de partida, comente `preparado` nesta issue.
