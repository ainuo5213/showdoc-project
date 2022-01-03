import dayjs from "dayjs";

const hello = ["早上好", "中午好", "下午好", "晚上好"];

export function greetHello(): string {
  const now = dayjs();
  const am8 = dayjs().hour(6).minute(0).format("YYYY-MM-DD HH:mm"); // 早上7点
  const noon11 = dayjs().hour(11).minute(0).format("YYYY-MM-DD HH:mm"); // 中午11点
  const afternoon14 = dayjs().hour(14).minute(0).format("YYYY-MM-DD HH:mm"); // 下午14点
  const night19 = dayjs().hour(19).minute(0).format("YYYY-MM-DD HH:mm"); // 下午19点

  const isMorning = now.isAfter(am8) && now.isBefore(noon11);
  const isNoon = now.isAfter(noon11) && now.isBefore(afternoon14);
  const isAfternoon = now.isAfter(afternoon14) && now.isBefore(night19);
  if (isMorning) {
    return hello[0];
  }
  if (isNoon) {
    return hello[1];
  }
  if (isAfternoon) {
    return hello[2];
  }
  return hello[3];
}
