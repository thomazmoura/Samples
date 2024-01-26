FROM mcr.microsoft.com/devcontainers/dotnet:8.0

USER root
RUN curl -s https://ohmyposh.dev/install.sh | bash -s

USER vscode
COPY  profile.ps1 /home/vscode/.config/powershell/Microsoft.PowerShell_profile.ps1
COPY  devcontainer.omp.json /home/vscode/devcontainer.omp.json

WORKDIR /home/vscode
RUN mkdir "/home/vscode/neovim" && \
    mkdir -p "/home/vscode/.local/bin" && \
    wget https://github.com/neovim/neovim/releases/download/v0.9.1/nvim-linux64.tar.gz -O "/home/vscode/nvim-linux64.tar.gz" && \
    tar -xzf ./nvim-linux64.tar.gz && \
    mv ./nvim-linux64/* "/home/vscode/neovim/" && \
    chmod +x "/home/vscode/neovim/bin/nvim" && \
    ln -s "/home/vscode/neovim/bin/nvim" "/home/vscode/.local/bin/nvim" && \
    rm -rf ./nvim-linux64.tar.gz && \
    rm -rf ./nvim-linux64

CMD pwsh