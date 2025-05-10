====== WorkLog ======

= 8/5/2025 =
> Design time hop system (2 hour)
    - พอนึกถึงช่วงเวลาต่างๆ เช้า เย็น กลางคืน จึงได้น้ำ State Pattern มาใช้
        และคิดว่าเป็นระบบหลักใหญ่ๆจึงนำ Singleton และ Observer Pattern มาใช้เพื่อให้ script ต่างๆสามารถใช้เหตุการ์นี้ได้
    - ปัญหาที่เจอหลักๆคึอการทำให้เปลื่ยนเวลาอย่างสมูท ในรูปแบบ UI

> Make time hop system (2 hour)
> Make time hop feeling and test (2 hour)


= 9/5/2025 =
> Design inventory system (1 hour 30 minute)
  - คิดว่าควรมี slot เป็นสื่อกลางเพื่อให้สามารถเอาไปนำไปใช้ประโยชน์ได้หลายอย่าง
    - ความท้าทายที่เจอคือการออกแบบให้ไอเทม เพิ่ม ลด สลับ ได้อย่างไม่มีปัญหา

> Make inventory system (2 hour)
> Make inventory bar (1 hour 30 minute)
    - ปัญหาเกี่ยวกับวิธีเชือม UI เข้ากับไอเทม Slot ต่างๆ

> Swap item (2 hour)
  - ไอเทมลากแล้วไม่สามารถสลับตำแหน่งได้ (แนวทางแก้ไข)

> Item Sorting (30 minute)
> Fix bug and Optimize (30 minute)
    - แก้ไขโค้ดที่ดูมีการเขียนซ้ำ และ แก้บัคเกี่ยวกับการแสดงผลที่ผิดพลาดในหน้า UI

= 10/5/2025 =
> Item use and Equip (1 hour)
  - สร้าง slot สำหรับสวใส่ไอเทมแยกมาเพื่อให้สามารถระบุไอเทมที่ใช้อยู่ได้

> Combat System (3  hour)
  - สร้าง EnemySpawnManager ที่สามารถควบคุม enemy ตามเวลาของ TimeHopSystem
  - สร้าง SlimeAI ที่มีความสามารถตามที่ระบุ

> Game UI (1 hour)
> Optimize and Fix bug (1 hour)