FROM rabbitmq:4.0.7-management

# Update and install curl
RUN apt-get update && apt-get install -y curl

# Download the RabbitMQ plugin and place it directly into /opt/rabbitmq/plugins
RUN curl -L https://github.com/rabbitmq/rabbitmq-delayed-message-exchange/releases/download/v4.0.7/rabbitmq_delayed_message_exchange-v4.0.7.ez \
    -o /opt/rabbitmq/plugins/rabbitmq_delayed_message_exchange-v4.0.7.ez

# Enable the plugin
RUN rabbitmq-plugins enable --offline rabbitmq_delayed_message_exchange