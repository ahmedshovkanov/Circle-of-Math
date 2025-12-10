// Пример unit-теста

import 'package:break_out_water_challenge/core/services/push_request_control.dart';
import 'package:flutter_test/flutter_test.dart';

int add(int a, int b) {
  return a + b;
}

void main() {
  test('add function test', () {
    print("test1");
    print("==============================================\n");

    expect(test1(), equals(true));

    print("test2");
    print("==============================================\n");
    expect(test2(), equals(false));

    print("test3");
    print("==============================================\n");

    expect(test3(), equals(false));

    print("test4");
    print("==============================================\n");

    expect(test4(), equals(true));

    // print("test5");
    // print("==============================================\n");

    // var data = PushRequestData();
    // PushRequestControl.acceptPushRequest(data);
    // expect(json.decode(json.encode(data)), equals(data));
  });
}

bool test1() {
  var data = PushRequestData();

  return PushRequestControl.shouldShowPushRequest(data);
}

bool test2() {
  var data = PushRequestData();
  PushRequestControl.acceptPushRequest(data);

  return PushRequestControl.shouldShowPushRequest(data);
}

bool test3() {
  var data = PushRequestData();
  PushRequestControl.declinePushRequest(data);

  return PushRequestControl.shouldShowPushRequest(data);
}

bool test4() {
  var data = PushRequestData();
  var date = DateTime.now().subtract(const Duration(days: 3));
  print(date.toIso8601String());
  PushRequestControl.declinePushRequest(data, date);
  data.Print();

  return PushRequestControl.shouldShowPushRequest(data);
}

void ShowPushRequest() {
  print("Show push Request");
}

/// первый запуск - показать запрос
/// если согласился то то не показывать больше
/// если отказался то показать еще раз через 3 дня
