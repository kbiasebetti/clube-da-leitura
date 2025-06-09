<div align=center>

# Projeto Clube da Leitura
</div>

![](https://imgur.com/99sH3l2.gif)

## Introdu��o
O **Clube da Leitura** � um sistema de gerenciamento simples, focado no controle de revistas organizadas em caixas. Desenvolvido como um projeto educacional em C#, ele simula o funcionamento de uma pequena biblioteca de revistas, com opera��es de cadastro, edi��o, exclus�o e visualiza��o de registros.

O sistema permite gerenciar de forma integrada todas as entidades principais de uma biblioteca: `Revistas`, `Caixas` de armazenamento, `Amigos` leitores e os `Empr�stimos` que conectam todos eles.

## Funcionalidades do Sistema
A aplica��o � dividida em m�dulos, cada um com sua responsabilidade, garantindo um sistema organizado e coeso.

- **Gest�o de Revistas:** Cadastro de todo o acervo de revistas, incluindo informa��es como t�tulo, edi��o e ano.
- **Gest�o de Caixas:** Permite organizar as revistas em caixas, cada uma identificada por uma cor e etiqueta.
- **Gest�o de Amigos:** Gerenciamento completo dos dados dos amigos que pegam revistas emprestadas, incluindo nome, respons�vel e contato.
- **Gest�o de Empr�stimos:** O m�dulo central que orquestra as opera��es, permitindo:
    - Registrar a sa�da de uma revista para um amigo.
    - Registrar a devolu��o de uma revista.
    - Visualizar o hist�rico de empr�stimos, incluindo os que est�o em aberto.

## Arquitetura e Padr�es de Projeto
A arquitetura do projeto � um dos seus pilares, projetada para ser escal�vel, reutiliz�vel e de f�cil manuten��o.

- **Modelo em 3 Camadas:** Toda a aplica��o segue este padr�o, separando claramente as responsabilidades:
    - **Apresenta��o (`Tela`):** Camada respons�vel pela interface com o usu�rio (menus, entrada e sa�da de dados).
    - **Dom�nio (`Entidade`):** Onde as classes de neg�cio (Amigo, Revista, etc.) e suas regras s�o definidas.
    - **Dados (`Reposit�rio`):** Camada de acesso a dados, respons�vel por armazenar, recuperar e gerenciar as informa��es em mem�ria.
- **Reutiliza��o de C�digo com Heran�a:** Para evitar a repeti��o de c�digo, foram criadas classes base (`EntidadeBase`, `RepositorioBase`, `TelaBase`) que centralizam as l�gicas comuns de CRUD e de interface, sendo herdadas por todos os m�dulos.
- **Regras de Neg�cio e Valida��es:** Cada m�dulo possui suas pr�prias regras de valida��o (campos obrigat�rios, formatos, etc.) e de neg�cio (controle de duplicidade, integridade de dados), garantindo a consist�ncia do sistema.

## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=github,git,cs,dotnet)](https://skillicons.dev)

## Como Usar o Sistema
1. Ao executar o programa, o menu principal ser� exibido com todos os m�dulos dispon�veis (Amigos, Revistas, Empr�stimos, etc.).
2. Selecione o m�dulo com o qual deseja interagir.
3. Um submenu espec�fico para aquele m�dulo aparecer� com as opera��es (cadastrar, editar, etc.).
4. Siga as instru��es no console para realizar a opera��o desejada.

## Instala��o e Execu��o

### Requisitos
- **.NET SDK** (recomendado .NET 8.0 ou superior)
- **Visual Studio** ou um editor de c�digo compat�vel (como VS Code)

### Passos

1. **Clonar o Reposit�rio:**
   ```bash
   git clone https://github.com/kbiasebetti/clube-da-leitura.git
   ```

2. **Acessar o diret�rio do projeto:**
   ```bash
   cd clube-da-leitura
   ```

3. **Restaurar depend�ncias:**
   ```bash
   dotnet restore
   ```

4. **Compilar e executar:**
   ```bash
   dotnet run
   ```