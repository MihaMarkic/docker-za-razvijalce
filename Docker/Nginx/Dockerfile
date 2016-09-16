FROM nginx:stable
COPY nginx.conf /etc/nginx/nginx.conf
#COPY nginx.key /etc/nginx/ssl/
#COPY nginx.crt /etc/nginx/ssl/
COPY fastcgi_params /etc/nginx/fastcgi_params

EXPOSE 80

#RUN cat /etc/nginx/nginx.conf