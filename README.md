<div align=center>

# Projeto Clube da Leitura
</div>

![](https://imgur.com/99sH3l2.gif)

## Introdução
O **Clube da Leitura** é um sistema de gerenciamento simples, focado no controle de revistas organizadas em caixas. Desenvolvido como um projeto educacional em C#, ele simula o funcionamento de uma pequena biblioteca de revistas, com operações de cadastro, edição, exclusão e visualização de registros.

O sistema permite gerenciar de forma integrada todas as entidades principais de uma biblioteca: `Revistas`, `Caixas` de armazenamento, `Amigos` leitores e os `Empréstimos` que conectam todos eles.

## Funcionalidades do Sistema
A aplicação é dividida em módulos, cada um com sua responsabilidade, garantindo um sistema organizado e coeso.

- **Gestão de Revistas:** Cadastro de todo o acervo de revistas, incluindo informações como título, edição e ano.
- **Gestão de Caixas:** Permite organizar as revistas em caixas, cada uma identificada por uma cor e etiqueta.
- **Gestão de Amigos:** Gerenciamento completo dos dados dos amigos que pegam revistas emprestadas, incluindo nome, responsável e contato.
- **Gestão de Empréstimos:** O módulo central que orquestra as operações, permitindo:
    - Registrar a saída de uma revista para um amigo.
    - Registrar a devolução de uma revista.
    - Visualizar o histórico de empréstimos, incluindo os que estão em aberto.

## Arquitetura e Padrões de Projeto
A arquitetura do projeto é um dos seus pilares, projetada para ser escalável, reutilizável e de fácil manutenção.

- **Modelo em 3 Camadas:** Toda a aplicação segue este padrão, separando claramente as responsabilidades:
    - **Apresentação (`Tela`):** Camada responsável pela interface com o usuário (menus, entrada e saída de dados).
    - **Domínio (`Entidade`):** Onde as classes de negócio (Amigo, Revista, etc.) e suas regras são definidas.
    - **Dados (`Repositório`):** Camada de acesso a dados, responsável por armazenar, recuperar e gerenciar as informações em memória.
- **Reutilização de Código com Herança:** Para evitar a repetição de código, foram criadas classes base (`EntidadeBase`, `RepositorioBase`, `TelaBase`) que centralizam as lógicas comuns de CRUD e de interface, sendo herdadas por todos os módulos.
- **Regras de Negócio e Validações:** Cada módulo possui suas próprias regras de validação (campos obrigatórios, formatos, etc.) e de negócio (controle de duplicidade, integridade de dados), garantindo a consistência do sistema.

## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=github,git,cs,dotnet)](https://skillicons.dev)

## Como Usar o Sistema
1. Ao executar o programa, o menu principal será exibido com todos os módulos disponíveis (Amigos, Revistas, Empréstimos, etc.).
2. Selecione o módulo com o qual deseja interagir.
3. Um submenu específico para aquele módulo aparecerá com as operações (cadastrar, editar, etc.).
4. Siga as instruções no console para realizar a operação desejada.

## Instalação e Execução

### Requisitos
- **.NET SDK** (recomendado .NET 8.0 ou superior)
- **Visual Studio** ou um editor de código compatível (como VS Code)

### Passos

1. **Clonar o Repositório:**
   ```bash
   git clone https://github.com/kbiasebetti/clube-da-leitura.git
   ```

2. **Acessar o diretório do projeto:**
   ```bash
   cd clube-da-leitura
   ```

3. **Restaurar dependências:**
   ```bash
   dotnet restore
   ```

4. **Compilar e executar:**
   ```bash
   dotnet run
   ```