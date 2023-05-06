FROM mysql:latest

ENV MYSQL_DATABASE=dict_set
ENV MYSQL_USER=DictSetUserDB
ENV MYSQL_PASSWORD=1234
ENV MYSQL_ROOT_PASSWORD=rootpassword

ADD dict_set_db_scheme.sql /docker-entrypoint-initdb.d/

EXPOSE 3306

CMD ["mysqld", "--init-file=/docker-entrypoint-initdb.d/dict_set_db_scheme.sql"]
