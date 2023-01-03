import dotenv from 'dotenv';

// Set the NODE_ENV to 'development' by default
process.env.NODE_ENV = process.env.NODE_ENV || 'development';

const envFound = dotenv.config();
if (envFound.error) {
  // This error should crash whole process

  throw new Error("⚠️  Couldn't find .env file  ⚠️");
}

export default {
  /**
   * Environment
   */
  ENV: process.env.NODE_ENV,

  /**
   * Apache Kafka
   */
  apacheKafka: {
    broker: process.env.BROKER ?? 'localhost:9092',
    topicName: process.env.TOPIC ?? 'email',
    clientId: process.env.KAFKA_CLIENT_ID,
    groupId: process.env.KAFKA_GROUP
  },
  
};
