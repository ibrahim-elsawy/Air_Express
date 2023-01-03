import { Kafka, logCreator, logLevel, Message, Producer, ProducerBatch, TopicMessages } from 'kafkajs';
import config from '@/config';
import EmailServiceDTO from '../models/DTO/EmailServiceDTO';
import { Service } from 'typedi';


@Service()
export default class EmailService{
	private producer: Producer

	constructor() {
		this.producer = this.createProducer()
	}

	private async start(): Promise<void> {
		try {
			await this.producer.connect()
		} catch (error) {
			console.log('Error connecting the producer: ', error)
		}
	}

	private async shutdown(): Promise<void> {
		await this.producer.disconnect()
	}

	public async sendBatch(message: EmailServiceDTO): Promise<void> {
		await this.start();
		const kafkaMessage: Message = { value: JSON.stringify(message) };

		const topicMessages: TopicMessages = {
			topic: config.apacheKafka.topicName,
			messages: [kafkaMessage]
		}

		const batch: ProducerBatch = {
			topicMessages: [topicMessages]
		}

		await this.producer.sendBatch(batch);
		await this.shutdown();
	}

	private createProducer(): Producer {
		const kafka = new Kafka({
			clientId: config.apacheKafka.clientId,
			brokers: [config.apacheKafka.broker],
		})

		return kafka.producer()
	}
}