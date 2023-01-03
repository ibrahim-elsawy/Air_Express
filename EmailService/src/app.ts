import EmailConsumer from "./services/KafkaService";


const x = new EmailConsumer();

x.startBatchConsumer();