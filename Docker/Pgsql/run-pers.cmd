docker run --name sledilec-ps -e POSTGRES_PASSWORD=skrivnost -d -p 5432:5432 --volume /var/lib/docker/postgresql/data:/var/lib/postgresql/data sledilec-ps