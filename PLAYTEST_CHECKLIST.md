# PLAYTEST_CHECKLIST.md — Brinkshift

Mục tiêu: đánh giá gameplay trên thiết bị thật trước khi production sâu.

## Gate 0 — Touch Feel

Test trên ít nhất:
- 1 iPhone thật;
- 1 Android thật.

Kiểm tra:
- [ ] Chạm lại không làm player teleport.
- [ ] Relative drag dễ hiểu.
- [ ] Player không bị ngón tay che.
- [ ] Sensitivity vừa đủ chính xác.
- [ ] Không có cảm giác trễ rõ rệt.
- [ ] Không có khác biệt khó chịu giữa FPS/refresh-rate thiết bị.
- [ ] Player không bị kẹt mép màn hình theo cách khó hiểu.
- [ ] Restart nhanh và ổn định.

### Gate 0 PASS
Chỉ tiếp tục Graze khi người chơi có thể điều khiển chính xác mà không phải “đấu với control”.

---

## Gate 1 — Graze Core

Kiểm tra:
- [ ] Người chơi hiểu rằng chạm hazard = chết.
- [ ] Người chơi nhận ra đi sát hazard có reward.
- [ ] Feedback Graze dễ nhận biết nhưng không che hazard.
- [ ] Hitbox có cảm giác công bằng.
- [ ] Không thể farm một obstacle bằng cách đứng cạnh nó.
- [ ] Multiplier/Chain Meter tạo động lực graze liên tục.
- [ ] Chơi an toàn lâu không phải chiến thuật ghi điểm tối ưu.
- [ ] Người chơi hiểu vì sao mình chết.

---

## Gate 1 — Player Validation 8–15 người

Không giải thích chiến thuật. Chỉ hướng dẫn thao tác nếu họ hoàn toàn không biết cách bắt đầu.

Ghi lại cho từng người:
- Số run họ tự nguyện chơi trước khi trả máy.
- Run thứ mấy xuất hiện graze chủ ý đầu tiên.
- Có tự nói/biểu hiện muốn chơi lại không.
- Khi chết: đổ lỗi cho mình hay cho game.
- Có cảm giác mình chơi tốt hơn sau vài run không.
- Có tránh xa mọi hazard vì Graze quá đáng sợ không.

### Heuristic hiện tại
Không phải benchmark ngành; dùng để hỗ trợ quyết định.

**CONTINUE** nếu phần lớn dấu hiệu tích cực:
- khoảng ≥ 8 run tự nguyện trung bình là tín hiệu mạnh;
- ≥ 60% người thử tự phát hiện/chủ động Graze là tín hiệu mạnh;
- phần lớn cái chết được hiểu là do thao tác/quyết định của người chơi;
- có biểu hiện rõ “thử lại”.

**ITERATE** nếu:
- control tốt nhưng Graze chưa đủ hấp dẫn;
- người chơi hiểu game nhưng cảm thấy unfair;
- họ chỉ chơi an toàn;
- feedback/multiplier chưa rõ.

**KILL/PIVOT** nếu sau nhiều vòng tuning hợp lý:
- phần lớn chỉ chơi 1–3 run;
- không ai muốn chủ động Graze;
- không có cảm giác mastery/retry;
- mechanic phải dựa vào meta/skin/reward bên ngoài mới có lý do chơi.

---

## Gate 2 — Viewer / Acquisition Test

Chỉ làm sau khi gameplay pass và có Art Direction v0.

Chuẩn bị 3 clip 10–15 giây.

Hỏi người chỉ xem:
- Bạn hiểu nhân vật đang cố làm gì không?
- Khoảnh khắc nào trông nguy hiểm nhất?
- Bạn có hiểu rằng đi sát nguy hiểm giúp được nhiều điểm hơn không?
- Bạn có muốn tự thử game không?

Quan sát:
- close-call có đọc được bằng mắt không;
- multiplier/feedback có dễ hiểu không;
- VFX có rối không;
- video có hook trong vài giây đầu không.

Nếu Gate 1 PASS nhưng Gate 2 FAIL: ưu tiên sửa presentation/visual hook trước khi thay core gameplay.

---

## Khi gửi kết quả về ChatGPT

Gửi:
- video quay màn hình nếu có;
- thiết bị/model;
- cảm giác control;
- điểm gây khó chịu;
- điều bạn thấy vui nhất;
- điều bạn thấy chán nhất;
- lỗi chức năng;
- phản ứng của tester.

ChatGPT sẽ quyết định bước tiếp theo: PASS / PASS WITH FIXES / ITERATE / KILL-PIVOT.
